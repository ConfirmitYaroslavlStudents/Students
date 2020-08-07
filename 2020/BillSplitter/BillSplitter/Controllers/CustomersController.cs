using System;
using System.Linq;
using BillSplitter.Controllers.Extensions;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersDbAccessor _customersDbAccessor;
        private readonly BillsDbAccessor _billsDbAccessor;

        public CustomersController(BillContext context)
        {
            _customersDbAccessor = new CustomersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Customers")]
        public IActionResult Index(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != this.GetUserId())
                throw new NotImplementedException("Case is not implemented yet");

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
                throw new NotImplementedException("Case is not implemented yet");
            
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
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != this.GetUserId())
                throw new NotImplementedException("Case is not implemented yet");

            _customersDbAccessor.DeleteById(customerId);

            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
