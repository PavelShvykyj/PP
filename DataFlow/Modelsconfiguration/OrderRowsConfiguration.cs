using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.ModelsConfiguration
{
    public class OrderRowsConfiguration : IEntityTypeConfiguration<OrderRows>
    {
        public void Configure(EntityTypeBuilder<OrderRows> builder) {

            builder.ToTable("o_rows");
            
            builder.HasKey(r => r.Id);

            //builder.HasIndex(r => r.OrderId);

            builder
                .Property(r => r.GoodId)
                .IsRequired();
            
            builder
                .HasOne(r => r.Good)
                .WithMany()
                .HasForeignKey(r => r.GoodId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(r => r.Order)
            //    .WithMany()
            //    .HasForeignKey(r => r.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(r => r.Price)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);
                //.HasPrecision(4);

            builder
                .Property(r => r.Quantity)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);
                //.HasPrecision( 4);

            builder
                .Property(r => r.Summ)
                .HasColumnType("decimal(15,4)")
                //.HasPrecision(4)
                .HasComputedColumnSql("[Quantity]*[Price]");
        }
    }
}
