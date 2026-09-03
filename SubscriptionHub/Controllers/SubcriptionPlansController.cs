using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan;
using SubscriptionHub.Application.SubscriptionPlans.Queries.GetAllSubscriptions;


namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class SubcriptionPlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubcriptionPlansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubscriptionPlanCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var subscriptionPlans = await _mediator.Send(new GetAllSubscriptionPlansQuery());

            return Ok(subscriptionPlans);
        }
    }
}
