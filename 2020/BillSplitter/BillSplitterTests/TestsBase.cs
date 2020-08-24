using System;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;

namespace BillSplitterTests
{
    public class TestsBase
    {
        public BillContext Context { get; }
        public TestsBase()
        {
            var opt =
                new DbContextOptionsBuilder<BillContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var context = new BillContext(opt);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Context = context;
        }
    }
}