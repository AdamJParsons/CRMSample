using CRMSample.Infrastructure.Common.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Faults
{
    public class ClientPolicy
    {
        public AsyncRetryPolicy<HttpResponseMessage> ImmediateHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; }
        public AsyncRetryPolicy<HttpResponseMessage> ExponentialHttpRetry { get; }
        public ClientPolicy(ILogger<ClientPolicy> logger, IOptions<ClientPolicySettings> policySettingsOptions)
        {
            var clientPolicySettings = policySettingsOptions.Value;

            int maxRetries = clientPolicySettings.MaxRetries > 0 ? clientPolicySettings.MaxRetries : 5;
            int retryInterval = clientPolicySettings.LinearRetryInterval > 100 ? clientPolicySettings.LinearRetryInterval : 1000;

            ImmediateHttpRetry = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .RetryAsync(
                    5,
                    (m, i) =>
                    {
                        logger.LogWarning("[{i}] Handling fault [{exception}] using Immediate Http Retry policy", i, m.Exception.Message);
                    });

            LinearHttpRetry = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    5,
                    retryAttempty => TimeSpan.FromSeconds(3),
                    (m, i) =>
                    {
                        logger.LogWarning("[{i}] Handling fault [{exception}] using Linear Http Retry policy", i, m.Exception.Message);
                    });

            ExponentialHttpRetry = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(res => !res.IsSuccessStatusCode)
                .WaitAndRetryAsync(
                    5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (m, i) =>
                    {
                        logger.LogWarning("[{i}] Handling fault [{exception}] using Exponential Http Retry policy", i, m.Exception.Message);
                    });
        }
    }
}
