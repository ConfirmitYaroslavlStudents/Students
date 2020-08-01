using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (!_billsDbAccessor.DbContains(billId))
                return View();//Можем че-то говорить, так возвращает вроде пустую страницу

            var customers = _billsDbAccessor.GetBillById(billId).Customers;

            return View(customers);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers")]
        public void Post(int billId)
        {
            if (!_billsDbAccessor.DbContains(billId))
                return;
            
            var customer = new Customer
            {
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

            _customersDbAccessor.AddCustomer(customer);
        }

        public int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Customers/{customerId}")]
        public IActionResult Delete(int billId, int customerId)
        {
            if (!_billsDbAccessor.DbContains(billId))
                return RedirectToAction("", "Customers", new {billId = billId});
            
            _customersDbAccessor.DeleteById(customerId);

            return RedirectToAction(nameof(Index), new { billId = billId });
        }


    }
}
