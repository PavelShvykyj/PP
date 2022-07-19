using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.ModelsConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder) {

            builder.ToTable("orders");
            
            builder
                .HasKey(o => o.Id)
                .IsClustered();

            builder
                .HasIndex(o => o.CustomerId)
                .IncludeProperties(new[] { "Id", "Summ" } );

            builder
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(o => o.Summ)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);

            builder
                .HasMany(o => o.Goods)
                .WithOne(r => r.Order)
                .HasForeignKey(r=>r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
