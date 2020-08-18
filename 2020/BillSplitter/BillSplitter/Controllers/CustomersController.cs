using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Customers")]
    public class CustomersController : BaseController
    {
        public CustomersController(UnitOfWork db) : base(db)
        {

        }
        
        [HttpGet]
        [ValidateUserAttributeFactory(RequestedRole = "Admin")]
        public IActionResult Index(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);

            ViewData["billId"] = billId;
            var customers = bill.Customers;

            return View(customers);
        }

        [HttpPost]
        public IActionResult AddCustomerOrContinueToPick(int billId) // give more suitable name
        {
            if (!Db.Bills.Exist(billId))
                Error();
            
            var userId = GetUserId();
            var bill = Db.Bills.GetBillById(billId);
            
            if (bill.Customers.FirstOrDefault(c => c.UserId == userId) == null)
            {
                var customer = new Customer
                {
                    BillId = billId,
                    UserId = GetUserId(),
                    Role = "Guest",
                    Name = GetUserName()
                };

                Db.Customers.Add(customer);
                Db.Save();
            }

            return RedirectToAction("PickPositions", "Positions", new {billId});
        }

        [HttpPost]
        [Route("{customerId}")]
        [ValidateUserAttributeFactory(RequestedRole = "Admin")]
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            Db.Customers.DeleteById(customerId);
            Db.Save();
            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
