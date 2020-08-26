using System;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;

namespace BillSplitterTests
{
    public abstract class TestsBase
    {
        protected BillContext Context { get; }

        protected TestsBase()
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