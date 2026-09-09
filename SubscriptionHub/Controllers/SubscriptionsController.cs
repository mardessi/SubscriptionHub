using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Subscription.Commands.CreateSubscription;
using SubscriptionHub.Application.Subscription.Queries;
using SubscriptionHub.Application.Subscription.Queries.GetAllSubscriptions;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateSubscriptionCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var subscription = await _mediator.Send(new GetSubscriptionByIdQuery { Id=id });
            return Ok(subscription);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllSubscriptionsQuery()));
        }
    }
}
