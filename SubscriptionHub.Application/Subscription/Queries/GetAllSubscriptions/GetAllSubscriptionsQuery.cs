using MediatR;

namespace SubscriptionHub.Application.Subscription.Queries.GetAllSubscriptions
{
    public record GetAllSubscriptionsQuery : IRequest<IEnumerable<SubscriptionDto>>
    {
    }
}
