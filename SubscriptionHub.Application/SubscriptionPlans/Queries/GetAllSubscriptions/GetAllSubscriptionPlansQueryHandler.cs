using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Interfaces;

namespace SubscriptionHub.Application.SubscriptionPlans.Queries.GetAllSubscriptions
{
    public class GetAllSubscriptionPlansQueryHandler : IRequestHandler<GetAllSubscriptionPlansQuery,IEnumerable<SubscriptionPlanDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetAllSubscriptionPlansQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<IEnumerable<SubscriptionPlanDto>> Handle(GetAllSubscriptionPlansQuery request, CancellationToken cancellationToken)
        { 

            return await _context.SubscriptionPlans
                .Where(c=>c.TenantId==_currentUserService.TenantId)
                .Select(p=> new SubscriptionPlanDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Currency = p.Currency,
                    BillingFrequency = p.BillingFrequency.ToString(),
                    IsActive = p.IsActive,
                }
                ).ToListAsync(cancellationToken);
        }
    }
}
