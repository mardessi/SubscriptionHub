using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Application.Invoice.Queries
{
    public record InvoiceDto
    {
        public Guid Id { get; init; }
        public Guid TenantId { get; init; }

        public Guid SubscriptionId { get; init; }

        public string Number { get; init; } = string.Empty;

        public string Status { get; init; } = string.Empty;
        public DateTime IssuedAt { get; init; }
        public DateTime DueDate { get; init; }
        public decimal TotalAmount { get; init; }

        public string Currency { get; init; } = string.Empty;
    }
}
