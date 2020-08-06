using System.Linq;
using BillSplitter.Controllers.Extensions;
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

        public OrdersController(BillContext context)
        {
            new CustomersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
            _ordersDbAccessor = new OrdersDbAccessor(context);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult AddOrder(int billId, int positionId)
        {
            var customer = _billsDbAccessor.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == this.GetUserId());

            _ordersDbAccessor.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId
            });

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }

        [Authorize]
        [HttpDelete]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult DeleteOrder(int billId, int positionId)
        {
            _ordersDbAccessor.DeleteByUserAndPosition(this.GetUserId(), positionId);

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }
    }
}
