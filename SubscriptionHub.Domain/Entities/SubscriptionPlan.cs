using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class SubscriptionPlan : AuditableEntity
    {
        public Guid TenantId { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public string? Description { get; private set; }

        public decimal Price { get; private set; }
        public string Currency { get; private set; } = string.Empty;

        public BillingFrequency BillingFrequency { get; private set; }
        public bool IsActive { get; private set; }

        private SubscriptionPlan()
        {
            
        }

        public SubscriptionPlan(Guid tenantId,string name,decimal price,string currency)
        {
            if (tenantId == Guid.Empty)
                throw new ArgumentException("TenantId is required.", nameof(tenantId));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (price <= 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));
            TenantId = tenantId;
            Name = name;
            Price = price;
            Currency = currency;
            BillingFrequency = BillingFrequency.Monthly;
            IsActive = true;

        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Name is required.", nameof(newName));
            Name = newName;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
            Price = newPrice;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
