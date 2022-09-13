using AutoMapper;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Dtos.Account;
using CRMSample.Domain.Identity.ViewModels.Account;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CRMSample.Application.Identity.Account.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserViewModel>
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IMediator _mediator;

        public RegisterUserCommandHandler(
            ILoginService loginService,
            IMapper mapper,
            IMediator mediator,
            ILogger<RegisterUserCommandHandler> logger)
        {
            _loginService = loginService;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<UserViewModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _loginService.FindByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new CrmApiException($"User [{request.Email}] already exists", HttpStatusCode.BadRequest);
            }

            if (!request.Password.Equals(request.ConfirmPassword))
            {
                throw new CrmApiException($"The password and confirm password values do not match", HttpStatusCode.BadRequest);
            }

            if (!request.Email.Equals(request.ConfirmEmail))
            {
                throw new CrmApiException($"The email and confirm email values do not match", HttpStatusCode.BadRequest);
            }

            var createUserRequest = new CreateUserRequest(request.UserName, request.Email, request.Password);

            var createdUser = await _loginService.CreateUserAsync(createUserRequest);

            if (createdUser == null)
            {
                throw new CrmApiException($"The user could not be created", HttpStatusCode.BadRequest);
            }

            var dto = _mapper.Map<ReadUserDto>(createdUser);

            var viewModel = new UserViewModel(dto);

            _logger.LogInformation("User [{id}]({username}) created successfully", createdUser.Id, createdUser.UserName);

            return viewModel;
        }
    }
}
