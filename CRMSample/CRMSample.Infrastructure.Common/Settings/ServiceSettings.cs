namespace CRMSample.Infrastructure.Common.Settings
{
    public class ServiceSettings
    {
        public string HostUrl { get; set; }
        public int Port { get; set; }
    }

    public class ServiceBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string EventBusConnection { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
