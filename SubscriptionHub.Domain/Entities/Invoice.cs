using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class Invoice : AuditableEntity
    {
        public Guid TenantId { get; private set; }

        public Guid SubscriptionId { get; private set; }

        public string Number { get; private set; } = string.Empty;

        public InvoiceStatus Status { get; private set; }
        public DateTime IssuedAt { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        private Invoice() { }

        public Invoice(Guid tenantId, Guid subscriptionId, string number, decimal totalAmount, string currency)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException(nameof(tenantId));
            if (subscriptionId == Guid.Empty)
                throw new ArgumentException(nameof(subscriptionId));
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Invoice number is required.", nameof(number));
            if (totalAmount <= 0)
                throw new ArgumentException("Total amount cannot be negative.", nameof(totalAmount));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));
            TenantId = tenantId;
            SubscriptionId = subscriptionId;
            Number = number;
            TotalAmount = totalAmount;
            Currency = currency;
            Status = InvoiceStatus.Pending;
            IssuedAt = DateTime.UtcNow;
            DueDate = IssuedAt.AddDays(30); // Default payment terms
        } 
        
        public void MarkAsPaid()
        {
            if (Status == InvoiceStatus.Cancelled)
                throw new InvalidOperationException("Only pending invoices can be marked as paid.");
            Status = InvoiceStatus.Paid;
        }

        public void MarkAsOverdue()
        {
            if (Status == InvoiceStatus.Cancelled || Status == InvoiceStatus.Paid)
                throw new InvalidOperationException("Only pending invoices can be marked as overdue.");
            Status = InvoiceStatus.Overdue;
        }
        public void Cancel()
        {
            if (Status == InvoiceStatus.Paid)
                throw new InvalidOperationException("Cannot cancel a paid invoice.");
            Status = InvoiceStatus.Cancelled;
        }

    }
}
