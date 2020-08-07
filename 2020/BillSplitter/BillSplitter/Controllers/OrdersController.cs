using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class OrdersController : SuperController
    {
        public OrdersController(BillContext context) : base(context)
        {
           
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult AddOrder(int billId, int positionId)
        {
            var customer = _uow.Bills.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == this.GetUserId());

            _uow.Orders.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId
            });
            _uow.Save();
            return RedirectToAction("PickPositions", "Positions", new { billId });
        }

        [Authorize]
        [HttpDelete]
        [Route("Bills/{billId}/Positions/{positionId}/Orders")]
        public IActionResult DeleteOrder(int billId, int positionId)
        {
            _uow.Orders.DeleteByUserAndPosition(this.GetUserId(), positionId);
            _uow.Save();
            return RedirectToAction("PickPositions", "Positions", new { billId });
        }
    }
}
