using MediatR;
using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Exceptions;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, TenantDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public GetTenantByIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        public async Task<TenantDto> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.Id!= _currentUserService.TenantId)
                throw new UnauthorizedAccessException();

            var tenant = await _context.Tenants.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
            if (tenant is null)
            {
                throw new NotFoundException("Tenant", request);
            }

            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                Slug = tenant.Slug,
                ContactEmail = tenant.ContactEmail,
                Status = tenant.Status.ToString()
            };
        }
    }
}
