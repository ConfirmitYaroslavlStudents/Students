using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class BillContext : DbContext
    {
        public BillContext(DbContextOptions<BillContext> options) : base(options)
        {

        }
        public BillContext()
        {

        }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.Customer)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder
                .Entity<Position>()
                .HasMany<Order>(e => e.Orders)
                .WithOne(e => e.Position)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
