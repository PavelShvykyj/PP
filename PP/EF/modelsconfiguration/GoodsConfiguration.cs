using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PP.EF.models;

namespace PP.EF.modelsconfiguration
{
    public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder) {

            builder.ToTable("goods");
            builder.HasKey(g => g.id);
            
            builder.Property(g => g.name)
                    .IsRequired()
                    .HasMaxLength(100);
            
            builder.Property(g => g.price)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnType("money");


            builder.Property(g => g.rest)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnType("real");
        }
    }
}
