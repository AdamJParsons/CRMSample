namespace CRMSample.Infrastructure.Common.Settings
{
    public class ServiceBusSettings
    {
        public bool AzureServiceBusEnabled { get; set; }
        public string EventBusConnection { get; set; }
        public int ConnectionTimeout { get; set; }
    }
}
