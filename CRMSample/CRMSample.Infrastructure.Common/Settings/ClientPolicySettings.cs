namespace CRMSample.Infrastructure.Common.Settings
{
    public class ClientPolicySettings
    {
        public int MaxRetries { get; set; } = 5;
        public int LinearRetryInterval { get; set; } = 1000;
    }
}
