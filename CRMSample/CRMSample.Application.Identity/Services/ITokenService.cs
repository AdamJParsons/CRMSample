using CRMSample.Domain.Identity.Entities.Account;

namespace CRMSample.Application.Identity.Services
{
    public interface ITokenService
    {
        Task<CreateTokenResult> CreateTokenAsync(ApplicationUser user);
    }
}
