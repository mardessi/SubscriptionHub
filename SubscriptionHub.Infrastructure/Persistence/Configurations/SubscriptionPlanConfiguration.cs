using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
             builder.ToTable("SubscriptionPlans");

             builder.HasKey(t => t.Id);

             builder.HasOne<Tenant>()
                    .WithMany()
                    .HasForeignKey(t => t.TenantId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);   // pas IsRequired() car nullable/optionnel

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");   // ⚠️ IMPORTANT pour decimal !

            builder.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(3);   // codes ISO comme "EUR", "USD" = 3 caractères

            builder.Property(p => p.BillingFrequency)
                .IsRequired();

            builder.Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
