using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BillsDbAccessor _billsDbAccessor;
        private readonly OrdersDbAccessor _ordersDbAccessor;
        private readonly UserIdVisitor _visitor;

        public OrdersController(BillContext context, UserIdVisitor visitor)
        {
            new CustomersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
            _ordersDbAccessor = new OrdersDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult AddOrder(int billId, int positionId)
        {
            var customer = _billsDbAccessor.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == _visitor.GetUserId(this));

            _ordersDbAccessor.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId
            });

            return RedirectToAction("Index", "Positions", new { billId, select = true });
        }

        [Authorize]
        [HttpDelete]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult DeleteOrder(int billId, int positionId)
        {
            _ordersDbAccessor.DeleteByUserAndPosition(_visitor.GetUserId(this), positionId);

            return RedirectToAction("Index", "Positions", new { billId, select = true });
        }
    }
}
