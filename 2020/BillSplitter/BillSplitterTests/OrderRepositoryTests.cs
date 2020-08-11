using BillSplitter.Data;
using BillSplitter.Models;
using System.Linq;
using Xunit;

namespace BillSplitterTests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void AddOrder_AddNewOrderInDb()
        {
            using var db = new InMemoryContextBuilder().Build();
            var uow = new UnitOfWork(db);
            var repo = uow.Orders;

            var order = new Order()
            {
                Id = 1
            };

            repo.AddOrder(order);
            uow.Save();

            Assert.True(db.Orders.Contains(order));
        }
    }
}
