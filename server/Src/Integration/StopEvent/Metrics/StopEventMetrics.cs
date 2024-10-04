using System.Diagnostics.Metrics;
using TransportProxy.Integration.Common;

namespace TransportProxy.Application.StopEvent.Metrics;

public class StopEventMetrics : IOjpMetrics
{
    private Histogram<long> ResponseTimeHistogram { get; }

    public StopEventMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(nameof(StopEventMetrics));
        ResponseTimeHistogram = meter.CreateHistogram<long>("response-time", "ms", "duration of stopEvent request");
    }

    public void SetResponseTime(long responseTime) => ResponseTimeHistogram.Record(responseTime);
}