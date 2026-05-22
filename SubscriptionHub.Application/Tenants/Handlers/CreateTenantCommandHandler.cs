using MediatR;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Application.Tenants.Commands;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Tenants.Handlers
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateTenantCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = new Tenant(request.Name, request.Slug, request.ContactEmail);
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync(cancellationToken);
            return tenant.Id;
        }
    }
}
