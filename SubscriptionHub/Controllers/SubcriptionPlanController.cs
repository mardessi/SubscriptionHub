using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan;
using SubscriptionHub.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById;
using SubscriptionHub.Application.Tenants.Commands;
using SubscriptionHub.Application.Tenants.Queries.GetTenantById;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SubcriptionPlanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubcriptionPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var subscriptionPlan = await _mediator.Send(new GetSubscriptionPlanByIdQuery { Id = id });

            return Ok(subscriptionPlan);
        }
    }
}
