using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
