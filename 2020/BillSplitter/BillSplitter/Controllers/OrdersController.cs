using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Positions/{positionId}/Orders")]
    public class OrdersController : BaseController
    {
        public OrdersController(UnitOfWork db) : base(db)
        {
           
        }

        [HttpPost]
        public IActionResult AddOrder(int billId, int positionId, [FromBody] Order order)
        {
            var customer = Db.Bills.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == this.GetUserId());
           
            Db.Orders.DeleteByUserAndPosition(this.GetUserId(), positionId);

            Db.Orders.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId,
                Quantity = order.Quantity//немного костяльно, лучше по-другому сделать
            });

            Db.Save();

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }

        [HttpDelete]
        public IActionResult DeleteOrder(int billId, int positionId)
        {
            Db.Orders.DeleteByUserAndPosition(this.GetUserId(), positionId);

            Db.Save();

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }
    }
}
