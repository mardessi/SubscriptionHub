using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(t => t.Id);

            builder.HasOne<Invoice>()
                   .WithMany()
                   .HasForeignKey(t => t.InvoiceId)
                   .OnDelete(DeleteBehavior.Restrict);
            
           builder.HasOne<Tenant>()
                   .WithMany()
                   .HasForeignKey(t => t.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.Property(t => t.Currency)
                   .IsRequired()
                   .HasMaxLength(3);

            builder.Property(t => t.Method)
                .IsRequired();

            builder.Property(t=>t.Status)
                .IsRequired();

        }
    }
}
