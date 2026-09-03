using MediatR;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Subscription.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, Guid>
    {
        private readonly IApplicationDbContext _dbcontext;
        private readonly ICurrentUserService _currentUserService;
        public CreateSubscriptionCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            _dbcontext = applicationDbContext;
            _currentUserService = currentUserService;
        }
        public async Task<Guid> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = new SubscriptionHub.Domain.Entities.Subscription(_currentUserService.TenantId, request.PlanId, request.CustomerEmail);

            _dbcontext.Subscriptions.Add(subscription);

            await _dbcontext.SaveChangesAsync(cancellationToken);

            return subscription.Id;
        }
    }
}
