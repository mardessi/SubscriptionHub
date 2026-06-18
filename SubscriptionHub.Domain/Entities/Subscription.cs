using SubscriptionHub.Domain.Common;
using SubscriptionHub.Domain.Enums;

namespace SubscriptionHub.Domain.Entities
{
    public class Subscription : AuditableEntity
    {
        public Guid TenantId { get; private set; }
        public Guid PlanId { get; private set; }
        public string CustomerEmail { get; private set; } = string.Empty;
        public SubscriptionStatus Status { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        private Subscription()
        {


        }

        public Subscription(Guid tenantId, Guid planId, string customerEmail)
        {
            if(tenantId == Guid.Empty)
                throw new ArgumentException(nameof(tenantId));
            if (planId == Guid.Empty) 
                throw new ArgumentException(nameof(planId));
            if(string.IsNullOrWhiteSpace(customerEmail))
                throw new ArgumentException("Customer email is required.", nameof(customerEmail)); 
            TenantId = tenantId;
            PlanId = planId;
            CustomerEmail = customerEmail;
            Status = SubscriptionStatus.Active;
            StartDate = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = SubscriptionStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
        }
        public void Reactivate()
        {
            if (Status == SubscriptionStatus.Cancelled || Status == SubscriptionStatus.Expired)
                throw new InvalidOperationException("Cannot reactivate a cancelled or expired subscription.");
            Status = SubscriptionStatus.Active;

        }
        public void Suspend()
        {
            if (Status == SubscriptionStatus.Cancelled || Status == SubscriptionStatus.Expired)
                throw new InvalidOperationException("Cannot suspend a cancelled or expired subscription.");
            Status = SubscriptionStatus.Suspended;
        }
        public void Expire()
        {
            Status = SubscriptionStatus.Expired;
            EndDate = DateTime.UtcNow;
        }
    }
}
