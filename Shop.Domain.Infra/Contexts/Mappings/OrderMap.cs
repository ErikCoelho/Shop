using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Domain.Infra.Contexts.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("SMALLDATETIME")
                .HasMaxLength(60);

            builder.Property(x => x.CustomerDoc)
                .IsRequired()
                .HasColumnName("Customer");

            builder.Property(x => x.Number)
                .IsRequired()
                .HasColumnName("Number")
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.DeliveryFee)
                .IsRequired()
                .HasColumnName("DeliveryFee")
                .HasColumnType("DECIMAL")
                .HasMaxLength(60);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status")
                .HasColumnType("DECIMAL")
                .HasMaxLength(60);

            builder.Ignore(x => x.Notifications);
        }
    }
}
