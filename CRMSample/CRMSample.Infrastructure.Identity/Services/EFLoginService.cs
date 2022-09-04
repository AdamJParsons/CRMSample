using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Entities.Account;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Identity.Services
{
    public class EFLoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EFLoginService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
