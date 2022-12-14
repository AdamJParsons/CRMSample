using CRMSample.Application.Identity.Account.Commands.Login;
using CRMSample.Domain.Common.ViewModels;
using CRMSample.Domain.Identity.ViewModels.Account;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRMSample.Services.Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // POST:    api/v1/Account/Login/{command}
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            _logger.LogInformation("Attempting login for user [{emailAddress}]", command.EmailAddress);

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}