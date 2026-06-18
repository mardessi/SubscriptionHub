using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Tenants.Commands;
using SubscriptionHub.Application.Tenants.Queries.GetTenantById;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTenantCommand command )
        {
           var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new {id});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tenant = await _mediator.Send(new GetTenantByIdQuery { Id=id});

            return Ok(tenant);
        }
    }
}
