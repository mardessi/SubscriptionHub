using MediatR;

namespace SubscriptionHub.Application.Invoice.Commands
{
    public record CreateInvoiceCommand : IRequest<Guid>
    {
        public Guid TenantId { get; init; }
        public Guid SubscriptionId { get; init; }
        public string Number { get; init; } = string.Empty;

        public decimal TotalAmount { get; init; }
        public string Currency { get; init; } = string.Empty;
    }
}
