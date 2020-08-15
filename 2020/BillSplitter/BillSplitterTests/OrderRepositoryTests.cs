using System.Collections.Generic;
using BillSplitter.Data;
using BillSplitter.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BillSplitterTests
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void AddOrder_AddNewOrderInDb()
        {
            var orders = new List<Order>();

            var orderDbSet = DbSetMockBuilder.BuildDbSet(orders);

            var contextMock = new Mock<DbContext>();
            contextMock.Setup(c => c.Set<Order>()).Returns(orderDbSet);

            var db = new UnitOfWork(contextMock.Object);
            var repo = db.Orders;

            var order = new Order()
            {
                Id = 1
            };

            repo.AddOrder(order);
            db.Save();

            Assert.Contains(order, orders); // Maybe Exists method?
        }
    }
}
