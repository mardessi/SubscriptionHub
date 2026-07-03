using MediatR;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, Guid>
    {
        private readonly IApplicationDbContext _dbcontext;
        public CreateSubscriptionPlanCommandHandler(IApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<Guid> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var subscriptionPlan = new SubscriptionPlan(request.TenantId, request.Name, request.Price , request.Currency);
             _dbcontext.SubscriptionPlans.Add(subscriptionPlan);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return subscriptionPlan.Id;
        }
    }
}
