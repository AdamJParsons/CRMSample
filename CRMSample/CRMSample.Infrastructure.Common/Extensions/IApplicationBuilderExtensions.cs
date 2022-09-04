using CRMSample.Infrastructure.Common.Middleware.Metrics;
using CRMSample.Infrastructure.Common.Settings;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMetricMiddleware(this IApplicationBuilder builder, Action<MetricSettings> configureOptions)
        {
            // see: https://localhost:5001/metrics
            MetricSettings options = new MetricSettings();
            configureOptions(options);
            return builder.UseMiddleware<MetricMiddleware>(options);
        }
    }
}
