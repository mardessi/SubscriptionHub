using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class Payment : AuditableEntity
    {
        public Guid TenantId { get; private set; }
        public Guid InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = string.Empty;
        public PaymentMethod Method { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime? PaidAt { get; private set; }

        private Payment() { }

        public Payment(Guid tenantId, Guid invoiceId, decimal amount, string currency, PaymentMethod paymentMethod)
        {
            if(tenantId == Guid.Empty) throw new ArgumentException("TenantId is required.", nameof(tenantId));
            if(invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId is required.", nameof(invoiceId));
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));
            if(paymentMethod == PaymentMethod.None )
                throw new ArgumentException("PaymentMethod is required.", nameof(paymentMethod));


            TenantId = tenantId;
            InvoiceId = invoiceId;
            Amount = amount;
            Currency = currency;
            Method = paymentMethod;
            Status = PaymentStatus.Pending;
        }

        public void Complete()
        {
            if (Status == PaymentStatus.Refunded || Status == PaymentStatus.Failed)
                throw new InvalidOperationException("Only pending payments can be marked as completed.");
            Status = PaymentStatus.Completed;
            PaidAt = DateTime.UtcNow;
        }

        public void Fail()
        {
            if (Status == PaymentStatus.Completed || Status == PaymentStatus.Refunded)
                throw new InvalidOperationException("Only pending payments can be marked as failed.");
            Status = PaymentStatus.Failed;
        }

        public void Refund()
        {
            if (Status != PaymentStatus.Completed)
                throw new InvalidOperationException("Only completed payments can be refunded.");
            Status = PaymentStatus.Refunded;
        }
    }
}
