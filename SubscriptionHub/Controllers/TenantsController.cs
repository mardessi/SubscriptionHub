using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Tenants.Commands;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TenantsController(IMediator mediator)
        {
            _mediator=mediator;
        }
        public async Task<IActionResult> Create([FromBody] CreateTenantCommand command )
        {
           var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new {id});
        }
    }
}
