using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");

            builder.HasKey(t => t.Id);

            builder.HasOne<Tenant>()
                   .WithMany()
                   .HasForeignKey(t => t.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<SubscriptionPlan>()
                   .WithMany()  
                   .HasForeignKey(t => t.PlanId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.StartDate)
                   .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t=>t.CustomerEmail)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(t => t.EndDate);

        }
    }
}
