using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.Subscription.Queries.GetAllSubscriptions
{
    public class GetAllSubscriptionsQueryHandler : IRequestHandler<GetAllSubscriptionsQuery, IEnumerable<SubscriptionDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetAllSubscriptionsQueryHandler(IApplicationDbContext context ,ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _context = context;
        }
        public async Task<IEnumerable<SubscriptionDto>> Handle(GetAllSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Subscriptions
                .Where(c => c.TenantId == _currentUserService.TenantId)
                .Select(p => new SubscriptionDto
                {
                    CustomerEmail = p.CustomerEmail,
                    Id = p.Id,
                    PlanId = p.PlanId,
                    StartDate = p.StartDate,
                    Status = p.Status.ToString(),
                }).ToListAsync();
        }
    }
}
