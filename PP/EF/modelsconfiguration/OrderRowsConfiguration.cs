using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.EF.models;

namespace PP.EF.modelsconfiguration
{
    public class OrderRowsConfiguration : IEntityTypeConfiguration<OrderRows>
    {
        public void Configure(EntityTypeBuilder<OrderRows> builder) {

            builder.ToTable("o_rows");
            
            builder.HasKey(r => r.id);

            //builder.HasIndex(r => r.orderid);

            builder
                .Property(r => r.goodid)
                .IsRequired();
            
            builder
                .HasOne(r => r.good)
                .WithMany()
                .HasForeignKey(r => r.goodid)
                .OnDelete(DeleteBehavior.Restrict);

            //builder
            //    .HasOne(r => r.order)
            //    .WithMany()
            //    .HasForeignKey(r => r.orderid)
            //    .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(r => r.price)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);
                //.HasPrecision(4);


            builder
                .Property(r => r.quantity)
                .HasColumnType("decimal(15,4)")
                .HasDefaultValue(0);
                //.HasPrecision( 4);



            builder
                .Property(r => r.summ)
                .HasColumnType("decimal(15,4)")
                //.HasPrecision(4)
                .HasComputedColumnSql("[quantity]*[price]");
        }
    }
}
