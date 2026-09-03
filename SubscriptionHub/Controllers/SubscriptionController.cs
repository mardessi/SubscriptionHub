using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Subscription.Commands.CreateSubscription;
using SubscriptionHub.Application.Subscription.Queries;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
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
    }
}
