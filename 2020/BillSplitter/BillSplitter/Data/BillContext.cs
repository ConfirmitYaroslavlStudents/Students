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

        public DbSet<Bill> Bill { get; set; }
    }
}
