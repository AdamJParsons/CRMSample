using CRMSample.Domain.Identity.ViewModels.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Identity.Account.Commands.Login
{
    public class LoginCommand : IRequest<UserViewModel>
    {
        public LoginCommand(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }
        public string Password { get; }
    }
}
