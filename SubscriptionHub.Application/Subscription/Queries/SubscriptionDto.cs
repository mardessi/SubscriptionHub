namespace SubscriptionHub.Application.Subscription.Queries
{
    public record SubscriptionDto
    {
        public Guid Id { get; init; }
        public Guid TenantId { get; init; }
        public Guid PlanId { get; init; }
        public string CustomerEmail { get; init; } = string.Empty;

        public DateTime StartDate { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
