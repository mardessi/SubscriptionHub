using MediatR;
using SubscriptionHub.Application.Common.Exceptions;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.Invoice.Queries
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
    {
        private readonly IApplicationDbContext _context;
        public GetInvoiceByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _context.Invoices.FindAsync(new object[] { request.Id }, cancellationToken);
            if (invoice == null)
                throw new NotFoundException(nameof(invoice), request.Id);
            return new InvoiceDto
            {
                Id = invoice.Id,
                Currency = invoice.Currency,
                DueDate = invoice.DueDate,
                IssuedAt = invoice.IssuedAt,
                Number = invoice.Number,
                Status = invoice.Status.ToString(),
                SubscriptionId = invoice.SubscriptionId,
                TenantId = invoice.TenantId,
                TotalAmount = invoice.TotalAmount,
            };
        }
    }
}
