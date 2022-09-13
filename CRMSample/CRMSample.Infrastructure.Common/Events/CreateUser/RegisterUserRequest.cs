using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Infrastructure.Common.Events.CreateUser
{
    public interface RegisterUserRequest
    {
        Guid IntegrationId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string ConfirmEmail { get; set; }
    }
}
