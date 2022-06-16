﻿using Microsoft.EntityFrameworkCore;
using DataTier.Models;
using DataTier.Modelsconfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataTier
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Good> Goods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRows> OrderRows { get; set; }

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
            
        }
    }
}
