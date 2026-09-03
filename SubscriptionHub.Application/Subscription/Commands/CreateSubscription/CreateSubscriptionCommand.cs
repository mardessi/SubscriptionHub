using MediatR;

namespace SubscriptionHub.Application.Subscription.Commands.CreateSubscription
{
    public record CreateSubscriptionCommand : IRequest<Guid>
    {
        //public Guid TenantId { get; init; }
        public Guid PlanId { get; init; }
        public string CustomerEmail { get; init; } = string.Empty;
    }
}
