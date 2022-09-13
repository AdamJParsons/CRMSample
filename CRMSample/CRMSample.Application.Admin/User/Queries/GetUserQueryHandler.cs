using AutoMapper;
using CRMSample.Application.Admin.Services;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.ViewModels.User;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CRMSample.Application.Admin.User.Queries
{
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserQueryHandler> _logger;

        public GetUserQueryHandler(IUserService userService, IMapper mapper, ILogger<GetUserQueryHandler> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(request.Id);

            if (user == null)
            {
                throw new CrmApiException($"User [{request.Id}] does not exist", HttpStatusCode.NotFound);
            }

            var dto = _mapper.Map<ReadUserDto>(user);

            _logger.LogInformation("User [{id}]({username}) retrieved successfully", user.Id, user.UserName);

            return new UserViewModel(dto);
        }
    }
}
