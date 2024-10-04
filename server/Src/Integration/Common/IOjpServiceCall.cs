using System.Diagnostics;
using System.Net;
using System.Text;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Scriban;
using TransportProxy.Integration.Common;

namespace TransportProxy.Application.Common;

public interface IOjpServiceCall<T, R> { }

public static class OjpServiceCallExtensions
{
    public static async Task<T> GetDataFromOjpService<T, R>(
        this IOjpServiceCall<T, R> service,
        R request, string? ojpServiceToken, string xmlTemplatePath,
        HttpClient httpClient, ILogger logger, IHttpContextAccessor httpContextAccessor, IOjpMetrics meters)
    {
        if (string.IsNullOrEmpty(ojpServiceToken))
            throw new CustomAuthenticationException("apiErrors.auth.tokenMissing");

        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ojpServiceToken);

        try
        {
            var template = Template.Parse(File.ReadAllText(xmlTemplatePath), xmlTemplatePath);
            
            var templatingWatch = new Stopwatch();  
            templatingWatch.Start();
            var result = template.Render(request);
            templatingWatch.Stop();
            logger.LogInformation($"----------->> {templatingWatch.ElapsedMilliseconds.ToString()}");
            
            var content = new StringContent(result, Encoding.UTF8, "application/xml");
            var watch = new Stopwatch();  
            watch.Start();  
            var response = await httpClient.PostAsync("", content);
            watch.Stop();  
            var responseTimeForCompleteRequest = watch.ElapsedMilliseconds; 
            httpContextAccessor.HttpContext.Response.Headers.Append($"X-Ojp-Service-{typeof(R).Name}-Response-Time-ms", responseTimeForCompleteRequest.ToString());

            meters.SetResponseTime(responseTimeForCompleteRequest);
            
            response.EnsureSuccessStatusCode(); // Ensure the request was successful
            var data = await response.Content.ReadAsStringAsync();

            var deserializeWatch = new Stopwatch();  
            deserializeWatch.Start();
            var ojp = Deserializer.DeserializeXml<T>(data);
            deserializeWatch.Stop();
            logger.LogInformation($"============>> {deserializeWatch.ElapsedMilliseconds.ToString()}");

            return ojp;
        }
        catch (Scriban.Syntax.ScriptRuntimeException ex)
        {
            logger.LogError(ex.Message);
            throw new CustomInternalException("apiErrors.templating.runtimeError", ex);
        }
        catch (Scriban.Syntax.ScriptArgumentException ex)
        {
            logger.LogError(ex.Message);
            throw new CustomInternalException("apiErrors.templating.argumentError", ex);
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex.ToString());
            
            switch (ex.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                    throw new CustomForbiddenException("apiErrors.ojp.forbidden", ex);
                case HttpStatusCode.TooManyRequests:
                    throw new CustomTooManyRequestsException("apiErrors.ojp.tooManyRequests", ex);
                default:
                    throw new CustomInternalException("apiErrors.ojp.generic", ex);
            }
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex.Message);
            throw new CustomInternalException("apiErrors.deserialize.generic", ex);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            throw new CustomInternalException("apiErrors.generic", ex);
        }
    }
}