using ExceleTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExceleTech.Infrastructure
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderLineItem> OrderLineItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLineItem> BasketLineItems { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
            // испровить
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
