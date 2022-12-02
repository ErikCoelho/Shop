using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Domain.Infra.Contexts.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.OwnsOne(x => x.Name)
                .Ignore(x => x.Notifications)
                .Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired(true);

            builder.OwnsOne(x => x.Name)
                .Ignore(x => x.Notifications)
                .Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired(true);

            builder.Property(x => x.Image)
                .IsRequired(false);

            builder.OwnsOne(x => x.Document)
                .Ignore(x => x.Notifications)
                .Property(x => x.Number)
                .HasColumnName("Document")
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .Ignore(x => x.Notifications)
                .Property(x => x.Address)
                .HasColumnName("Email")
                .IsRequired(true);

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasColumnName("PasswordHash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasColumnName("Slug")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            builder.Ignore(x => x.Notifications);

            builder
                .HasIndex(x => x.Slug, "IX_User_Slug")
                .IsUnique();
        }
    }
}
