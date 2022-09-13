namespace CRMSample.Application.Identity.Services
{
    public class CreateUserRequest
    {
        public CreateUserRequest(
            string userName,
            string emailAddress,
            string password)
        {
            UserName = userName;
            EmailAddress = emailAddress;
            Password = password;
        }

        public string UserName { get; }
        public string EmailAddress { get; }
        public string Password { get; }
    }
}
