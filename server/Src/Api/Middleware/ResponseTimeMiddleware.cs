using System.Diagnostics;
using Api.Metrics;

namespace Api.Middleware;

public class ResponseTimeMiddleware
{
    // Name of the Response Header, Custom Headers starts with "X-"  
    private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";

    // Handle to the next Middleware in the pipeline  
    private readonly RequestDelegate _next;
    private readonly RequestMetrics _meters;

    public ResponseTimeMiddleware(RequestDelegate next, RequestMetrics meters)
    {
        _next = next;
        _meters = meters;
    }

    public Task InvokeAsync(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();
        context.Response.OnStarting(() =>
        {
            watch.Stop();
            var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
            context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
            _meters.SetResponseTime(responseTimeForCompleteRequest);
            return Task.CompletedTask;
        });
        // Call the next delegate/middleware in the pipeline   
        return this._next(context);
    }
}