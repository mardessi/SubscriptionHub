using MediatR;

namespace SubscriptionHub.Application.Tenants.Queries.GetTenantById
{
    public record TenantDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Slug { get; init; } = string.Empty;
        public string ContactEmail { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
    }
}
