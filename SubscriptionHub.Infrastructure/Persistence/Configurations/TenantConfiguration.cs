using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t=>t.Slug)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t=>t.ContactEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.HasIndex(t => t.Slug)
                .IsUnique();
        }
    }
}
