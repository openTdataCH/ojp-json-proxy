using System.Diagnostics.Metrics;
using OpenTelemetry.Metrics;

namespace Api.Metrics;

public class RequestMetrics
{
    private Histogram<long> ResponseTimeHistogram { get; }

    public RequestMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(nameof(RequestMetrics));
        ResponseTimeHistogram = meter.CreateHistogram<long>("response-time", "ms", "duration of whole request");
    }

    public void SetResponseTime(long responseTime) => ResponseTimeHistogram.Record(responseTime);
}