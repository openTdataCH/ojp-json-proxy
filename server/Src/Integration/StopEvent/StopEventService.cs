using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TransportProxy.Application.Common;
using TransportProxy.Application.StopEvent.Metrics;

namespace TransportProxy.Application.StopEvent;


public class StopEventService : IOjpServiceCall<OjpStopEvent, StopEventRequestModel>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<StopEventService> _logger;
    private readonly StopEventMetrics _meters;

    public StopEventService(IHttpClientFactory httpClientFactory, ILogger<StopEventService> logger, StopEventMetrics meters)
    {
        _httpClient = httpClientFactory.CreateClient("OjpServiceV1");
        _logger = logger;
        _meters = meters;
    }

    public async Task<OjpStopEvent> GetOjpStopEvent(StopEventRequestModel stopEvent, string? ojpServiceToken, IHttpContextAccessor httpContextAccessor)
    {
        const string xmlTemplate = "StopEvent/Templates/StopEventRequestTemplate.xml";

        return await this.GetDataFromOjpService(stopEvent, ojpServiceToken, xmlTemplate, _httpClient, _logger, httpContextAccessor, _meters);
    }
}