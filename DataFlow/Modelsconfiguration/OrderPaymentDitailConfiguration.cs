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
    public class OrderPaymentDitailConfiguration : IEntityTypeConfiguration<OrderPaymentDitail>
    {
        public void Configure(EntityTypeBuilder<OrderPaymentDitail> builder)
        {
            builder.HasIndex(p=> p.ExternalID);

            builder
                .HasOne(p => p.Order)
                .WithOne()
                .HasForeignKey<OrderPaymentDitail>(p => p.OrderId);
            builder.Property(p => p.ExternalID).IsRequired();
        }
    }
}