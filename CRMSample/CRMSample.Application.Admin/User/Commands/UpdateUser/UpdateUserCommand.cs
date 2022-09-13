using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.ViewModels.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Admin.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserViewModel>
    {
        public UpdateUserCommand(UpdateUserDto updateUserDto)
        {
            User = updateUserDto;
        }

        public UpdateUserDto User { get; }
    }
}
