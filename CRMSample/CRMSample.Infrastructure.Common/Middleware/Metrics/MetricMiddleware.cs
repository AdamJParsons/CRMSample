using CRMSample.Infrastructure.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Prometheus;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Middleware.Metrics
{
    public class MetricMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MetricSettings _options;

        public MetricMiddleware(RequestDelegate next, MetricSettings options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path.Value;
            var method = httpContext.Request.Method;

            var requestTotalConfiguration = new CounterConfiguration
            {
                LabelNames = new[] { "path", "method", "status" }
            };

            var counter = Prometheus
                .Metrics
                .CreateCounter(
                    $"prometheus_{_options.ApiName}_request_total", 
                    "HTTP Requests Total",
                    requestTotalConfiguration);

            Gauge httpInProgress = Prometheus
                .Metrics
                .CreateGauge(
                    $"prometheus_{_options.ApiName}_http_requests_in_progress",
                    "Number of requests in progress", 
                    "system");

            Histogram httpRequestsDuration = Prometheus
                .Metrics
                .CreateHistogram(
                    $"prometheus_{_options.ApiName}_http_requests_duration_seconds", 
                    "Duration of http requests per tracking system", 
                    "system");

            var statusCode = 200;

            using var inprogress = httpInProgress.TrackInProgress();
            using var timer = httpRequestsDuration.NewTimer();

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception)
            {
                statusCode = 500;
                counter.Labels(path, method, statusCode.ToString()).Inc();
                CommonMetrics.ExceptionsOccur.Inc();
                throw;
            }

            if (path != "/metrics")
            {
                statusCode = httpContext.Response.StatusCode;
                counter.Labels(path, method, statusCode.ToString()).Inc();
            }
        }
    }
}
