using CRMSample.Domain.Admin.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Admin.ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel(ReadUserDto dto)
        {
            User = dto;
        }

        public ReadUserDto User { get; }
    }
}
