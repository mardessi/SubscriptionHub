using MediatR;

namespace SubscriptionHub.Application.Subscription.Queries
{
    public record GetSubscriptionByIdQuery : IRequest<SubscriptionDto>
    {
        public Guid Id { get; init; }
    
    }
}
