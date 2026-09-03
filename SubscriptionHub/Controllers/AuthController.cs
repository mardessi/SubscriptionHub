using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Auth.Commands.Login;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthController : ControllerBase
    {   
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
