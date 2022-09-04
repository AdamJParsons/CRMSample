using CRMSample.Infrastructure.Common.Settings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Middleware.HealthChecks
{
    public class ExternalEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _settings;

        public ExternalEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(_settings.HostUrl);

            // the serivice is down
            if (reply.Status != IPStatus.Success) return HealthCheckResult.Unhealthy();

            // if it takes more than 0.5s then the service is up but not performant
            if (reply.RoundtripTime > 500) return HealthCheckResult.Degraded();

            // the service is healthy
            return HealthCheckResult.Healthy();
        }
    }
}
