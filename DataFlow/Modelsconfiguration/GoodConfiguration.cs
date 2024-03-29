﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.ModelsConfiguration
{
    public class GoodConfiguration : IEntityTypeConfiguration<Good>
    {
        public void Configure(EntityTypeBuilder<Good> builder) {

            builder.ToTable("goods");
            
            builder.HasKey(g => g.Id);
            
            builder.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            
            builder.Property(g => g.Price)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnType("money");

            builder.Property(g => g.Rest)
                .HasDefaultValue(0)
                .IsRequired()
                .HasColumnType("real");
        }
    }
}
