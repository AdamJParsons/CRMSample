using CRMSample.Domain.Admin.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.Admin.Services
{
    public interface IUserService
    {
        Task<ApplicationUserModel?> GetUserByIdAsync(long id, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApplicationUserModel?> GetUserByEmailAsync(string emailAddress, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApplicationUserModel?> GetUserByUserNameAsync(string username, CancellationToken cancellationToken = default(CancellationToken));

        Task CreateAsync(ApplicationUserModel model, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
