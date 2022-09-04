namespace CRMSample.Application.Identity.Services
{
    public record CreateTokenResult
    {
        public CreateTokenResult(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}