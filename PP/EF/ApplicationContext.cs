using Microsoft.EntityFrameworkCore;
using PP.EF.models;
using PP.EF.modelsconfiguration;

namespace PP.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderRows> OrderRows { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Server =.\\SQLEXPRESS; Database = PetProj; User Id=sa;Password=123;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new GoodsConfiguration());
            modelBuilder.ApplyConfiguration(new CustomersConfiguration());
        }
    }
}
