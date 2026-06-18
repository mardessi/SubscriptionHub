using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(t => t.Id);

            builder.HasOne<Subscription>()
                   .WithMany()
                   .HasForeignKey(t => t.SubscriptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Tenant>()
                   .WithMany()
                   .HasForeignKey(t => t.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(t=>t.Number)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(t => t.Number)
                .IsUnique();

            builder.Property(t=>t.Status)
                .IsRequired();

            builder.Property(t=>t.IssuedAt)
                .IsRequired();

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Currency)
                .IsRequired()
                .HasMaxLength(3);   

        }
    }
}
