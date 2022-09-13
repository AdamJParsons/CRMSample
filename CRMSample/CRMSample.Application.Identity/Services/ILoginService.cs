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
        Task<ApplicationUser> CreateUserAsync(CreateUserRequest request);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password);
    }
}
