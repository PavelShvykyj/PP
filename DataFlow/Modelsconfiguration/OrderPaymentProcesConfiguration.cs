using DataTier.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.ModelsConfiguration
{
    public class OrderPaymentProcesConfiguration : IEntityTypeConfiguration<OrderPaymentProces>
    {
        public void Configure(EntityTypeBuilder<OrderPaymentProces> builder)
        {
            builder.HasIndex(p=> p.ExternalId);

            builder
                .HasOne(p => p.Order)
                .WithOne()
                .HasForeignKey<OrderPaymentProces>(p => p.OrderId);

            builder.Property(p => p.ExternalName).HasMaxLength(100);
            builder.Property(p => p.ExternalId).IsRequired();
        }
    }
}