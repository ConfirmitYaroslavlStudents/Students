﻿using BillSplitter.Models;
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
                .Entity<User>()
                .HasMany(u => u.Customers)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Bill>()
                .HasMany(b => b.Positions)
                .WithOne(p => p.Bill)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
