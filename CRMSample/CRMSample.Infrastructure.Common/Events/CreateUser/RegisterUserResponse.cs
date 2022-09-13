namespace CRMSample.Infrastructure.Common.Events.CreateUser
{
    public interface RegisterUserResponse
    {
        public bool IsSuccess { get; set; }
        public Guid IntegrationId { get; set; }
        public string Errors { get; set; }
    }
}
