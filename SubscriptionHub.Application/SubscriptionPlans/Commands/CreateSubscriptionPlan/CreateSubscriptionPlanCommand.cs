using MediatR;
using System.Globalization;

namespace SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public record CreateSubscriptionPlanCommand : IRequest<Guid>
    {
        //public Guid TenantId { get; init; }
        public decimal Price { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Currency { get; init; } = string.Empty;
    }
}
