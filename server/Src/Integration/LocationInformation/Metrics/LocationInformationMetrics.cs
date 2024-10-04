using System.Diagnostics.Metrics;
using Microsoft.Extensions.Configuration;
using TransportProxy.Integration.Common;

namespace TransportProxy.Application.LocationInformation.Metrics;

public class LocationInformationMetrics : IOjpMetrics
{
    private Histogram<long> ResponseTimeHistogram { get; }

    public LocationInformationMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(nameof(LocationInformationMetrics));
        ResponseTimeHistogram = meter.CreateHistogram<long>("response-time", "ms", "duration of locationInformation request");
    }

    public void SetResponseTime(long responseTime) => ResponseTimeHistogram.Record(responseTime);
}