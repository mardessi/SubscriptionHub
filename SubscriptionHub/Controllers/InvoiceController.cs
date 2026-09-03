using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubscriptionHub.Application.Invoice.Commands;
using SubscriptionHub.Application.Invoice.Queries;

namespace SubscriptionHub.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var inoive = await _mediator.Send(new GetInvoiceByIdQuery { Id = id });

            return Ok(inoive);
        }
    }
}
