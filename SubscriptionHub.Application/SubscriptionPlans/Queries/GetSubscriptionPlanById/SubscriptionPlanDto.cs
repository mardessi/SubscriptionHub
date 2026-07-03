namespace SubscriptionHub.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    public record SubscriptionPlanDto
    {
        public Guid Id { get; init; }
        public Guid TenantId { get; init; }
        public string Name { get; init ; } = string.Empty;
        public decimal Price { get; init; }
        public string Currency { get; set; } = string.Empty;
    }
}
