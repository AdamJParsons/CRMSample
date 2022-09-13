using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Common.Data.User
{
    public interface IUser
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
