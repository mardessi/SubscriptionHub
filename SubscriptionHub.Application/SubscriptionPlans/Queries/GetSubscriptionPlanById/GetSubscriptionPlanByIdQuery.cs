using MediatR;

namespace SubscriptionHub.Application.SubscriptionPlans.Queries.GetSubscriptionPlanById
{
    public record GetSubscriptionPlanByIdQuery : IRequest<SubscriptionPlanDto>
    {
        public Guid Id { get; init; }
    
    }
}
