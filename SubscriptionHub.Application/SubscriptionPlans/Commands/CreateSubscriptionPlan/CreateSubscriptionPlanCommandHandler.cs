using MediatR;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.SubscriptionPlans.Commands.CreateSubscriptionPlan
{
    public class CreateSubscriptionPlanCommandHandler : IRequestHandler<CreateSubscriptionPlanCommand, Guid>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly ICurrentUserService _currentUserService;
        public CreateSubscriptionPlanCommandHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbcontext = dbContext;
            _currentUserService = currentUserService;
        }
        public async Task<Guid> Handle(CreateSubscriptionPlanCommand request, CancellationToken cancellationToken)
        {
            var subscriptionPlan = new SubscriptionPlan(_currentUserService.TenantId, request.Name, request.Price , request.Currency);
             _dbcontext.SubscriptionPlans.Add(subscriptionPlan);
            await _dbcontext.SaveChangesAsync(cancellationToken);
            return subscriptionPlan.Id;
        }
    }
}
