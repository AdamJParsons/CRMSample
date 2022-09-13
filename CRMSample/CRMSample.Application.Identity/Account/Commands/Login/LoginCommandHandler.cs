using AutoMapper;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Dtos.Account;
using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Domain.Identity.ViewModels.Account;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CRMSample.Application.IdentityTests")]
namespace CRMSample.Application.Identity.Account.Commands.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, UserViewModel>
    {
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public LoginCommandHandler(ILoginService loginService, ITokenService tokenService, IMapper mapper, ILogger<LoginCommandHandler> logger)
        {
            _logger = logger;
            _loginService = loginService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _loginService.FindByEmailAsync(request.EmailAddress);

            if (user == null)
            {
                throw new CrmApiException($"User with email address [{request.EmailAddress}] does not exist", HttpStatusCode.NotFound);
            }

            bool isValid = await _loginService.ValidateCredentialsAsync(user, request.Password);

            if (!isValid)
            {
                throw new CrmApiException($"Invalid username or password", HttpStatusCode.NotFound);
            }

            var accessToken = await _tokenService.CreateTokenAsync(user);

            var dto = _mapper.Map<ApplicationUser, ReadUserDto>(user);

            dto.AccessToken = accessToken.Token;

            var viewModel = new UserViewModel(dto);

            _logger.LogInformation("User [{id}]({username}) successfully logged in", user.Id, user.UserName);

            return viewModel;
        }
    }
}
