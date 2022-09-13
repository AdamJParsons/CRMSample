namespace CRMSample.Application.Common.Events.CreateUser
{
    public interface RegisterUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
