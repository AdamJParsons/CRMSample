using CRMSample.Application.Common.Events.CreateUser;
using CRMSample.Domain.Admin.Entities.User;

namespace CRMSample.Application.Admin.Services
{
    public interface IUserIntegrationService
    {
        Task<RegisterUserResponse> RegisterAsync(ApplicationUserModel model, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(ApplicationUserModel model, CancellationToken cancellationToken = default(CancellationToken));
    }
}
