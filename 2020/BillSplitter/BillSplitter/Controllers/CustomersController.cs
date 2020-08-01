using System;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomersDbAccessor _customersDbAccessor;
        private readonly UsersDbAccessor _usersDbAccessor;
        private readonly BillsDbAccessor _billsDbAccessor;
        private UserIdVisitor _visitor;
        public CustomersController(BillContext context, UserIdVisitor visitor)
        {
            _customersDbAccessor = new CustomersDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Customers")]
        public IActionResult Index(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != _visitor.GetUserId(this))
                throw new NotImplementedException("Case is not implemented yet");

            var customers = bill.Customers;

            return View(customers);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers")] // What route must be for this action?
        public void Post(int billId)
        {
            if (!_billsDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            var customer = new Customer
            {
                BillId = billId,
                UserId = _visitor.GetUserId(this),
                Name = _usersDbAccessor.GetUserById(_visitor.GetUserId(this)).Name
            };

            _customersDbAccessor.AddCustomer(customer);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers/{customerId}")]
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != _visitor.GetUserId(this))
                throw new NotImplementedException("Case is not implemented yet");

            _customersDbAccessor.DeleteById(customerId);

            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
