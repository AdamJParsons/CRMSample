using CRMSample.Application.Admin.Services;
using CRMSample.Application.Common.Events.CreateUser;
using CRMSample.Domain.Admin.Entities.User;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CRMSample.Infrastructure.Admin.Services
{
    public class UserIntegrationService : IUserIntegrationService
    {
        private readonly IRequestClient<RegisterUserRequest> _registerUserClient;
        private readonly ILogger<UserIntegrationService> _logger;

        public UserIntegrationService(
            IRequestClient<RegisterUserRequest> registerUserClient,
            ILogger<UserIntegrationService> logger)
        {
            _registerUserClient = registerUserClient;
            _logger = logger;
        }

        public Task<RegisterUserResponse> RegisterAsync(ApplicationUserModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUserModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
