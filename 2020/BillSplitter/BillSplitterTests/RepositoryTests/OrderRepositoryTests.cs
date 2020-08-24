using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests.RepositoryTests
{
    public class OrderRepositoryTests : TestsBase
    {
        [Fact]
        public void AddOrder_AddNewOrderInDb()
        {
            var db = new UnitOfWork(Context);
            var repo = db.Orders;

            var order = new Order()
            {
                Id = 1
            };

            repo.AddOrder(order);
            db.Save();

            Assert.Contains(order, Context.Orders); // Maybe Exists method?
        }
    }
}
