using AutoMapper;
using CRMSample.Application.Admin.Services;
using CRMSample.Application.Admin.User.Commands.CreateUser;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Common.Services;
using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.ViewModels.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CRMSample.Application.Admin.User.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly ILogger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(
            IUserService userService,
            IMapper mapper,
            IDateTime dateTime,
            ILogger<UpdateUserCommandHandler> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _dateTime = dateTime;
            _logger = logger;
        }

        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.User.Id, cancellationToken);

            if(user == null)
            {
                throw new CrmApiException($"User [{request.User.Id}] does not exist", HttpStatusCode.NotFound);
            }

            // check if email has changed and whether it is in use
            if(!user.EmailAddress.Equals(request.User.EmailAddress, StringComparison.OrdinalIgnoreCase))
            {
                // email address has changed.
                // check if it is in use
                var userWithMatchingEmail = await _userService.GetUserByEmailAsync(request.User.EmailAddress);
                if(userWithMatchingEmail != null)
                {
                    throw new CrmApiException($"Email {request.User.EmailAddress} is already in use", HttpStatusCode.BadRequest); 
                }
            }

            // check if username has changed and whether it is in use
            if (!user.UserName.Equals(request.User.UserName, StringComparison.OrdinalIgnoreCase))
            {
                // username has changed.
                // check if it is in use
                var userWithMatchingUsername = await _userService.GetUserByUserNameAsync(request.User.UserName);
                if (userWithMatchingUsername != null)
                {
                    throw new CrmApiException($"Username {request.User.UserName} is already in use", HttpStatusCode.BadRequest);
                }
            }

            // update the user
            UpdateUserDto updateUser = request.User;
            user.UpdateFrom(updateUser, _dateTime.Now);

            // commit the changes via the unit of work
            await _userService.SaveChangesAsync(cancellationToken);

            // map and return the saved user
            var userDto = _mapper.Map<ReadUserDto>(user);
            return new UserViewModel(userDto);
        }
    }
}
