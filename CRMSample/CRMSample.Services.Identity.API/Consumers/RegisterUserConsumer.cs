using CRMSample.Application.Common.Events.CreateUser;
using CRMSample.Application.Identity.Account.Commands.RegisterUser;
using MassTransit;
using MediatR;

namespace CRMSample.Services.Identity.API.Consumers
{
    internal class RegisterUserConsumer : IConsumer<RegisterUserRequest>
    {
        private readonly IMediator _mediator;

        public RegisterUserConsumer(IMediator mediator)
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
