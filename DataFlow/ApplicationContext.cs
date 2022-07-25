using Microsoft.EntityFrameworkCore;
using DataTier.Models;
using DataTier.ModelsConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataTier
{

    public  class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRows> OrderRows { get; set; }
        public DbSet<OrderPaymentProces> OrderPaymentProceses { get; set; }
        public DbSet<OrderPaymentDitail> OrderPaymentDetails { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server=db;Database=PetProj;User Id=sa;Password=Mylocalhost88!;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PetProj;User Id=sa;Password=123;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new GoodConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderRowsConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPaymentProcesConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPaymentDitailConfiguration());
            // ------  IMPORTENT -------> must tell identity to configure its entities
            // ------  else have error
            // The entity type IdentityUserLogin of string require a primary key to be defined
            base.OnModelCreating(modelBuilder);

        }
    }
}
