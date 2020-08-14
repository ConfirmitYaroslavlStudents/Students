﻿using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Validators;
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
        [ValidateUser]
        public IActionResult Index(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);

            ViewData["billId"] = billId;
            var customers = bill.Customers;

            return View(customers);
        }

        [HttpPost]
        public IActionResult Post(int billId) // give more suitable name
        {
            if (!Db.Bills.Exist(billId))
                Error();
            
            var userId = this.GetUserId();
            var bill = Db.Bills.GetBillById(billId);
            
            if (bill.Customers.FirstOrDefault(c => c.UserId == userId) == null)
            {
                var customer = new Customer
                {
                    BillId = billId,
                    UserId = this.GetUserId(),
                    Name = this.GetUserName()
                };

                Db.Customers.Add(customer);
                Db.Save();
            }

            return RedirectToAction("PickPositions", "Positions", new {billId});
        }

        [HttpPost]
        [Route("{customerId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int customerId) // TODO Maybe delete confirmation?
        {
            Db.Customers.DeleteById(customerId);
            Db.Save();
            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
