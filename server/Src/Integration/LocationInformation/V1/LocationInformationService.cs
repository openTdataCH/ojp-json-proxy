using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TransportProxy.Application.Common;
using TransportProxy.Application.LocationInformation.Metrics;

namespace TransportProxy.Application.LocationInformation.V1;

public class LocationInformationService : IOjpServiceCall<OjpLocationInformation, LocationInformationRequestModel>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LocationInformationService> _logger;
    private readonly LocationInformationMetrics _meters;

    public LocationInformationService(IHttpClientFactory httpClientFactory, ILogger<LocationInformationService> logger, LocationInformationMetrics meters)
    {
        _httpClient = httpClientFactory.CreateClient("OjpServiceV1");
        _logger = logger;
        _meters = meters;
    }

    public async Task<OjpLocationInformation> GetOjpLocationInformation(LocationInformationRequestModel locationInformation,
        string? ojpServiceToken, IHttpContextAccessor httpContextAccessor)
    {
        const string xmlTemplate = "LocationInformation/Templates/V1/LocationInformationRequestTemplate.xml";

        return await this.GetDataFromOjpService(locationInformation, ojpServiceToken, xmlTemplate, _httpClient, _logger, httpContextAccessor, _meters);
    }
}