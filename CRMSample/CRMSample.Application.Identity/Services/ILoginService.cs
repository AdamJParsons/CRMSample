using CRMSample.Domain.Identity.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Identity.Services
{
    public interface ILoginService
    {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password);
    }

    public interface ITokenService
    {
        Task<CreateTokenResult> CreateTokenAsync(ApplicationUser user);
    }
}
