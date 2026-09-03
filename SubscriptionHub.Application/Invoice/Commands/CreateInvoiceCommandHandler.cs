using MediatR;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.Invoice.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateInvoiceCommandHandler(IApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = new SubscriptionHub.Domain.Entities.Invoice(request.TenantId, request.SubscriptionId, request.Number, request.TotalAmount, request.Currency);

            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return invoice.Id;

        }
    }
}
