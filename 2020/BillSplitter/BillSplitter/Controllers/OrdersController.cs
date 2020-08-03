using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class OrdersController : Controller
    {
        private CustomersDbAccessor _customersDbAccessor;
        private BillsDbAccessor _billsDbAccessor;
        private OrdersDbAccessor _ordersDbAccessor;
        private UserIdVisitor _visitor;

        public OrdersController(BillContext context, UserIdVisitor visitor)
        {
            _customersDbAccessor = new CustomersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}/Positions/{positionId}/Orders")]
        public IActionResult AddOrder(int billId, int positionId)
        {
            var customer = _billsDbAccessor.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == _visitor.GetUserId(this));

            _ordersDbAccessor.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId
            });

            return RedirectToAction("Index", "Positions", new {billId, select = true});
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}/Positions/{positionId}/Orders/{orderId}")]
        public IActionResult DeleteOrder(int billId, int positionId, int orderId)
        {
            _ordersDbAccessor.DeleteById(orderId);
            return RedirectToAction("Index", "Positions", new { billId, select = true });
        }
    }
}
