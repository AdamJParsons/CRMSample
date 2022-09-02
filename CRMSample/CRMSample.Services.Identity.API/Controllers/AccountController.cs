using CRMSample.Application.Identity.Account.Commands;
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

        public async Task<IActionResult> LoginAsync([FromBody] LoginCommand command)
        {
            _logger.LogInformation("Attempting login for user [{emailAddress}]", command.EmailAddress);

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}