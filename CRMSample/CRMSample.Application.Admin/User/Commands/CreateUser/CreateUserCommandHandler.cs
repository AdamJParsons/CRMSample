using AutoMapper;
using CRMSample.Application.Admin.Services;
using CRMSample.Application.Common.Events.CreateUser;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Common.Services;
using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.Entities.Person;
using CRMSample.Domain.Admin.Entities.User;
using CRMSample.Domain.Admin.ViewModels.User;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("CRMSample.Application.AdminTests")]
namespace CRMSample.Application.Admin.User.Commands.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserViewModel>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IRequestClient<RegisterUserRequest> _registerUserClient;
        private readonly IBusControl _bus;

        public CreateUserCommandHandler(
            IUserService userService,
            IMapper mapper,
            IDateTime dateTime,
            IRequestClient<RegisterUserRequest> registerUserClient,
            IBusControl bus,
            ILogger<CreateUserCommandHandler> logger)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _dateTime = dateTime;
            _registerUserClient = registerUserClient;
            _bus = bus;
        }

        public async Task<UserViewModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userService.GetUserByUserNameAsync(request.UserName, cancellationToken);

            if (existingUser != null)
            {
                throw new CrmApiException($"User with username [{request.UserName}] already exists", HttpStatusCode.NotFound);
            }

            existingUser = await _userService.GetUserByEmailAsync(request.EmailAddress, cancellationToken);

            if (existingUser != null)
            {
                throw new CrmApiException($"User with email [{request.EmailAddress}] already exists", HttpStatusCode.NotFound);
            }

            ApplicationUserModel user = new ApplicationUserModel
            {
                IntegrationId = Guid.NewGuid(),
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                JobTitle = request.JobTitle,
                DateCreated = _dateTime.Now,
                DateModified = _dateTime.Now,
                Person = new PersonModel
                {
                    GenderId = request.GenderId,
                    TitleId = request.TitleId,
                    Forename = request.Forename,
                    Surname = request.Surname,
                    OtherNames = request.OtherNames,
                    DateModified = _dateTime.Now,
                    DateCreated = _dateTime.Now
                }
            };

            await _userService.CreateAsync(user, cancellationToken);

            await _userService.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ReadUserDto>(user);

            _logger.LogInformation("User [{id}]({username}) created successfully", user.Id, user.UserName);

            // Register the user with the identity layer
            var registerUserResponse = await _registerUserClient.GetResponse<RegisterUserResponse>(new
            {
                IntegrationId = user.IntegrationId,
                UserName = user.UserName,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword,
                Email = request.EmailAddress,
                ConfirmEmail = request.ConfirmEmailAddress
            }, cancellationToken);

            //// todo: what if the operation fails?
            if (registerUserResponse != null)
            {
                var response = registerUserResponse.Message;
                if (!response.IsSuccess)
                {
                    // handle the error(s) here
                    // we may want to delete the user we created and throw an api exception
                }
            }

            return new UserViewModel(dto);
        }
    }
}
