using CRMSample.Domain.Identity.ViewModels.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Identity.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserViewModel>
    {
        public RegisterUserCommand(
            Guid integrationId,
            string userName, 
            string email, 
            string confirmEmail, 
            string password, 
            string confirmPassword)
        {
            IntegrationId = integrationId;
            UserName = userName;
            Email = email;
            ConfirmEmail = confirmEmail;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public Guid IntegrationId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string ConfirmEmail { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
    }
}
