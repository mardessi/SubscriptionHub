using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SubscriptionHub.Domain.Entities;

namespace SubscriptionHub.Infrastructure.Persistence.Configurations
{
    public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.ToTable("InvoiceLines");
            
            builder.HasKey(t => t.Id);

            builder.HasOne<Invoice>()
                   .WithMany()
                   .HasForeignKey(t => t.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(t => t.Description)
                .IsRequired()
                     .HasMaxLength(500);

            builder.Property(t => t.Quantity)
                .IsRequired();

            builder.Property(t=>t.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
