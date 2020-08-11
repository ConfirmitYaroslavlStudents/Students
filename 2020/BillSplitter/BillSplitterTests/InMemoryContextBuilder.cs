using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;

namespace BillSplitterTests
{
    class InMemoryContextBuilder
    {
        public BillContext Build()
        {
            var opt = 
                new DbContextOptionsBuilder<BillContext>()
                .UseInMemoryDatabase(databaseName: "BillContext").Options;

            var context = new BillContext(opt);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
