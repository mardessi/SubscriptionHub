using SubscriptionHub.Domain.Common;

namespace SubscriptionHub.Domain.Entities
{
    public class InvoiceLine : Entity
    {
        public Guid InvoiceId { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public decimal TotalPrice { get; private set; }

        private InvoiceLine() { }

        public InvoiceLine(Guid invoiceId, string description, int quantity, decimal unitPrice)
        {
            if (invoiceId == Guid.Empty) throw new ArgumentException("InvoiceId is required.",nameof(invoiceId));
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required.", nameof(description));
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            if (unitPrice <= 0) throw new ArgumentException("UnitPrice must be greater than zero.", nameof(unitPrice));

            InvoiceId = invoiceId;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }
    }
}
