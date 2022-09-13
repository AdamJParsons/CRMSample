using CRMSample.Application.Admin.User.Commands.CreateUser;
using CRMSample.Domain.Admin.ViewModels.User;
using CRMSample.Domain.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRMSample.Services.Admin.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost()]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Attempting to create new user [{username}]", command.UserName);

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}