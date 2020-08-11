using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Positions/{positionId}/Orders")]
    public class OrdersController : SuperController
    {
        public OrdersController(BillContext context) : base(context)
        {
           
        }

        [HttpPost]
        public IActionResult AddOrder(int billId, int positionId, [FromBody] Order order)
        {

            var customer = Uow.Bills.GetBillById(billId).Customers
                .FirstOrDefault(c => c.UserId == this.GetUserId());
           
            Uow.Orders.DeleteByUserAndPosition(this.GetUserId(), positionId);

            Uow.Orders.AddOrder(new Order()
            {
                CustomerId = customer.Id,
                PositionId = positionId,
                Quantity = order.Quantity//немного костяльно, лучше по-другому сделать
            });

            Uow.Save();

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }

        [HttpDelete]
        public IActionResult DeleteOrder(int billId, int positionId)
        {
            Uow.Orders.DeleteByUserAndPosition(this.GetUserId(), positionId);

            Uow.Save();

            return RedirectToAction("PickPositions", "Positions", new { billId });
        }
    }
}
