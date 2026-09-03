using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Tenant> Tenants { get; }
        DbSet<User> Users { get; }

        DbSet<SubscriptionPlan> SubscriptionPlans { get; }

        DbSet<SubscriptionHub.Domain.Entities.Subscription> Subscriptions { get; }
        DbSet<Payment> Payments { get; }
        DbSet<SubscriptionHub.Domain.Entities.Invoice> Invoices { get; }
        DbSet<InvoiceLine> InvoicesLines { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
