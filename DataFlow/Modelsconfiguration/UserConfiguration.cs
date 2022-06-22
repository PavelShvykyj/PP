using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataTier.Models;

namespace DataTier.Modelsconfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.Ignore(u => u.Customer);

            builder.HasOne(u => u.Customer)
                   .WithOne(c => c.User)
                   .HasForeignKey<Customer>(c => c.Id)
                   .HasPrincipalKey<User>(u => u.CustomerId);
        }
    }
}
