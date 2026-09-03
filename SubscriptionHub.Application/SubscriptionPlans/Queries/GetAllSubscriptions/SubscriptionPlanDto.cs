namespace SubscriptionHub.Application.SubscriptionPlans.Queries.GetAllSubscriptions
{
    public record SubscriptionPlanDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public decimal Price { get; init; }

        public string Currency { get; init; } = string.Empty;
        public string BillingFrequency { get; init; } = string.Empty;

        public bool IsActive { get; init; }

    }
}
