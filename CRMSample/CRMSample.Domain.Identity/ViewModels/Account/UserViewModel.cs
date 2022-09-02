using CRMSample.Domain.Identity.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Domain.Identity.ViewModels.Account
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
