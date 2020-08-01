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
        public CustomersController(BillContext context)
        {
            _customersDbAccessor = new CustomersDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _billsDbAccessor = new BillsDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("/Home/{userId}/Bill/{billId}/Manage/Customers")]
        public IActionResult Index(int userId, int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != userId)
                throw new NotImplementedException("Case is not implemented yet");

            var customers = bill.Customers;

            return View(customers);
        }

        [Authorize]
        [HttpPost]
        [Route("/Home/{userId}/Bill/{billId}/Customers")] // What route must be for this action?
        public void Post(int userId, int billId)
        {
            if (!_billsDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            var customer = new Customer
            {
                BillId = billId,
                UserId = userId,
                Name = _usersDbAccessor.GetUserById(userId).Name
            };

            _customersDbAccessor.AddCustomer(customer);
        }

        [Authorize]
        [HttpPost]
        [Route("/Home/{userId}/Bill/{billId}/Manage/Customers/{customerId}/Delete")]
        public IActionResult Delete(int userId, int billId, int customerId) // TODO Maybe delete confirmation?
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill.UserId != userId)
                throw new NotImplementedException("Case is not implemented yet");

            _customersDbAccessor.DeleteById(customerId);

            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
