using MediatR;

namespace SubscriptionHub.Application.SubscriptionPlans.Queries.GetAllSubscriptions
{
    public record GetAllSubscriptionPlansQuery : IRequest<IEnumerable<SubscriptionPlanDto>>
    {
    }
}
