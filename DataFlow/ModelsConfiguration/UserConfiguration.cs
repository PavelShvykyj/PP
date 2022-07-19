using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasOne(u => u.Customer)
                   .WithOne(c => c.User)
                   .HasForeignKey<User>(u => u.CustomerId);
        }
    }
}
