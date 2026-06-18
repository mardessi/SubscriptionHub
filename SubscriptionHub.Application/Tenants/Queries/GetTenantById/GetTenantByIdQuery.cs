using MediatR;

namespace SubscriptionHub.Application.Tenants.Queries.GetTenantById
{
    public record GetTenantByIdQuery : IRequest<TenantDto>
    {
        public Guid Id { get; init; }
    }
}
