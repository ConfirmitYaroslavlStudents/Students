using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;

namespace BillSplitter.Data
{
    public class BillContext : DbContext
    {
        public BillContext(DbContextOptions<BillContext> options) : base(options)
        {

        }
        public BillContext() : base()
        {

        }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
