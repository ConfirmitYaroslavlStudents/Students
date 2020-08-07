using System;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class CustomersController : SuperController
    {



        public CustomersController(BillContext context) : base(context)
        {

        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Customers")]
        [ValidateUser]
        public IActionResult Index(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);

            ViewData["billId"] = billId;
            var customers = bill.Customers;

            return View(customers);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers")] 
        public IActionResult Post(int billId)
        {
            if (!_billsDbAccessor.DbContains(billId))
                Error();
            
            var userId = this.GetUserId();
            var bill = _billsDbAccessor.GetBillById(billId);
            
            if (bill.Customers.FirstOrDefault(c => c.UserId == userId) == null)
            {
                var customer = new Customer
                {
                    BillId = billId,
                    UserId = this.GetUserId(),
                    Name = this.GetUserName()
                };

                _customersDbAccessor.AddCustomer(customer);
            }

            return RedirectToAction("PickPositions", "Positions", new {billId});
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers/{customerId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            var bill = _billsDbAccessor.GetBillById(billId);
           

            _customersDbAccessor.DeleteById(customerId);

            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
