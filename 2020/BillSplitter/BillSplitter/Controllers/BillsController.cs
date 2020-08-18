using System;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels.ViewBill;
using BillSplitter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills")]
    public class BillsController : BaseController
    {
        public BillsController(UnitOfWork db) : base(db)
        {
           
        }

        [HttpGet]
        [Route("~/")]
        public IActionResult Index()
        {

            List<BillViewModel> bills = Db.Bills.GetByCustomerUserId(GetUserId()).Select(b => new BillViewModel()
            {
                Bill = b,
                HasManageAccess = b.UserId == GetUserId()
            }).ToList();

            return View(bills);
        }

        [HttpPost]
        public IActionResult Create(Bill createdBill)
        {
            createdBill.UserId = GetUserId();
            var customer = new Customer
            {
                Name = GetUserName(),
                UserId = GetUserId(),
                Role = "Admin",
                Bill = createdBill
            };

            Db.Bills.Add(createdBill);
            Db.Customers.Add(customer);

            Db.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{billId}")]
        public IActionResult ViewBill(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (customer == null)
                return View("JoinBill", bill);

            var customersBill = new CustomerBillBuilder().Build(customer);

            var model = BuildVewModelForViewBill(bill, customersBill);

            return View(model);
        }

        private BillViewModel BuildVewModelForViewBill(Bill bill, List<Position> customersBill)
        {
            var positions = bill.Positions
                .Select(position => new PositionViewModel(position))
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var pos in positions)
            {
                var customerPosition = customersBill
                    .FirstOrDefault(p => p.Id == pos.Id);
                if (customerPosition != null)
                {
                    pos.ActualPrice = customerPosition.Price;
                    pos.Selected = true;
                }
            }

            var model = new BillViewModel
            {
                Bill = bill,
                Positions = positions,
                HasManageAccess = bill.UserId == GetUserId(), // TODO: Update with roles, I wont update this, because this requires to update views 
                CustomerSum = customersBill.Sum(p => p.Price)
            };

            return model;
        }

        [HttpGet]
        [Route("{billId}/Join")]
        public IActionResult JoinBill(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);
          
            return View(bill);
        }

        [HttpPost]
        [Route("{billId}")]
        [ValidateUserAttributeFactory(RequestedRole = "Admin")]
        public IActionResult Delete(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);

            if (bill.UserId != GetUserId())
                throw new NotImplementedException("Case is not implemented yet");

            Db.Bills.DeleteById(billId);
            Db.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
