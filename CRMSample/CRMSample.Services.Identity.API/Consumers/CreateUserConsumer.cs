using CRMSample.Application.Identity.Account.Commands.RegisterUser;
using CRMSample.Infrastructure.Common.Events.CreateUser;
using MassTransit;
using MediatR;

namespace CRMSample.Services.Identity.API.Consumers
{
    internal class CreateUserConsumer : IConsumer<RegisterUserRequest>
    {
        private readonly IMediator _mediator;

        public CreateUserConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<RegisterUserRequest> context)
        {
            RegisterUserRequest request = context.Message;

            RegisterUserCommand command = new RegisterUserCommand(
                request.IntegrationId,
                request.UserName, 
                request.Email, 
                request.ConfirmEmail, 
                request.Password, 
                request.ConfirmPassword);

            var response = await _mediator.Send(command);

            if(response != null && response.User != null)
            {
                await context.RespondAsync<RegisterUserResponse>(new
                {
                    IsSuccess = true,
                    IntegrationId = response.User.IntegrationId
                });
            }
        }
    }
}
