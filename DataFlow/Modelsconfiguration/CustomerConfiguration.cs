using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.Modelsconfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) {

            builder.ToTable("customers");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(150);
        }
    }
}
