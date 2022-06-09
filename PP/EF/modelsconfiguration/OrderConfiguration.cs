using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.EF.models;

namespace PP.EF.modelsconfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder) {

            builder.ToTable("orders");
            
            builder
                .HasKey(o => o.id)
                .IsClustered();

            //builder
            //    .Property(o => o.id)
            //    .HasDefaultValueSql("GETDATE()");

            builder
                .HasIndex(o => o.customerid)
                .IncludeProperties(new[] { "id", "summ" } );


            builder
                .HasOne(o => o.customer)
                .WithMany()
                .HasForeignKey(o => o.customerid)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(o => o.summ)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);
            // ??? .HasValueGenerator();


            builder
                .HasMany(o => o.rows)
                .WithOne(r => r.order)
                .HasForeignKey(r=>r.orderid)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
