using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Customers")]
    public class CustomersController : SuperController
    {
        public CustomersController(BillContext context) : base(context)
        {

        }
        
        [HttpGet]
        [ValidateUser]
        public IActionResult Index(int billId)
        {
            var bill = Uow.Bills.GetBillById(billId);

            ViewData["billId"] = billId;
            var customers = bill.Customers;

            return View(customers);
        }

        [HttpPost]
        public IActionResult Post(int billId) // give more suitable name
        {
            if (!Uow.Bills.Exist(billId))
                Error();
            
            var userId = this.GetUserId();
            var bill = Uow.Bills.GetBillById(billId);
            
            if (bill.Customers.FirstOrDefault(c => c.UserId == userId) == null)
            {
                var customer = new Customer
                {
                    BillId = billId,
                    UserId = this.GetUserId(),
                    Name = this.GetUserName()
                };

                Uow.Customers.Add(customer);
                Uow.Save();
            }

            return RedirectToAction("PickPositions", "Positions", new {billId});
        }

        [HttpPost]
        [Route("{customerId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            var bill = Uow.Bills.GetBillById(billId);
           

            Uow.Customers.DeleteById(customerId);
            Uow.Save();
            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
