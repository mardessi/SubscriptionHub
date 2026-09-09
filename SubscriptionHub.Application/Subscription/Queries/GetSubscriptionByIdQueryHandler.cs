using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Exceptions;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.Subscription.Queries
{
    public class GetSubscriptionByIdQueryHandler : IRequestHandler<GetSubscriptionByIdQuery, SubscriptionDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        public GetSubscriptionByIdQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<SubscriptionDto> Handle(GetSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {


            var subscription = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == request.Id
                 && s.TenantId == _currentUserService.TenantId, cancellationToken);

            if (subscription == null)
            {
                throw new NotFoundException(nameof(SubscriptionHub.Domain.Entities.Subscription),request.Id);
            }

            return new SubscriptionDto
            {
                Id = subscription.Id,
                //TenantId = subscription.TenantId,
                CustomerEmail = subscription.CustomerEmail,
                StartDate = subscription.StartDate,
                PlanId = subscription.PlanId,
                Status = subscription.Status.ToString()
            };
        }
    }
}
