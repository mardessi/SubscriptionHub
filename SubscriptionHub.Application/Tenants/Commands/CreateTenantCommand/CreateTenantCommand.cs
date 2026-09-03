using MediatR;

namespace SubscriptionHub.Application.Tenants.Commands.CreateTenantCommand
{
    public record CreateTenantCommand : IRequest<Guid>
    {
        public string Name { get; init; } = string.Empty;
        public string Slug { get; init; } = string.Empty;
        public string ContactEmail { get; init; } = string.Empty;
    }
}
