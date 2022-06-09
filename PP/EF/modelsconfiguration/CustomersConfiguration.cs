using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.EF.models;

namespace PP.EF.modelsconfiguration
{
    public class CustomersConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> builder) {

            builder.ToTable("customers");

            builder.HasKey(c => c.id);

            builder.HasIndex(c => c.email).IsUnique();

            builder.Property(c => c.name)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(c => c.email)
                    .IsRequired()
                    .HasMaxLength(150);
        }
    }
}
