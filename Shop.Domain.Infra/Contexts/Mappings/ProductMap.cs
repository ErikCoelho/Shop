using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Domain.Infra.Contexts.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasColumnName("Description")
                .HasColumnType("TEXT");


            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("MONEY");

            builder.Property(x => x.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT");

            builder.Ignore(x => x.Notifications);

            builder.Property(x => x.Image)
                .IsRequired(false);


        }
    }
}
