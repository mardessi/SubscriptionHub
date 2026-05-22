using Microsoft.EntityFrameworkCore;
using SubscriptionHub.Application.Common.Interfaces;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence
{
    public class AppDbContext : DbContext,IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }
}
