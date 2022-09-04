using Prometheus;

namespace CRMSample.Infrastructure.Common.Middleware.Metrics
{
    public static class CommonMetrics
    {
        public static Counter ExceptionsOccur = Prometheus.Metrics.CreateCounter("exceptions_total", "Total number of exceptions for service lifetime", "system");
    }
}
