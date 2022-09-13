using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Entities.Account;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Identity.Services
{
    public class EFLoginService : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public EFLoginService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> ValidateCredentialsAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> CreateUserAsync(CreateUserRequest request)
        {
            ApplicationUser newUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.EmailAddress
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            // Instantiate the creation exception and errors, if necessary
            if (!result.Succeeded)
            {
                var exception = new CrmApiException($"Could not create user with [{newUser.UserName}]", HttpStatusCode.BadRequest);

                foreach (var error in result.Errors)
                {
                    var conduitError = new CrmApiError(error.Code, error.Description);
                    exception.ApiErrors.Add(conduitError);
                }

                throw exception;
            }

            return newUser;
        }
    }
}
