using System;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.LinqExtensions;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels;
using BillSplitter.Models.ViewModels.ViewBill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillsDbAccessor _billsDbAccessor;
        private readonly UsersDbAccessor _usersDbAccessor;
        private readonly UserIdVisitor _visitor;

        public BillsController(BillContext context, UserIdVisitor visitor)
        {
            _billsDbAccessor = new BillsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            BillsIndexModel model = new BillsIndexModel
            {
                AdminBills = _usersDbAccessor.GetUserById(_visitor.GetUserId(this)).Bills,
                CustomerBills = _billsDbAccessor.GetBillsByCustomerUserId(_visitor.GetUserId(this))
            };

            ViewData["userId"] = _visitor.GetUserId(this);
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/")]
        public IActionResult Create(Bill createdBill)
        {
            createdBill.UserId = _visitor.GetUserId(this);
            var customer = new Customer
            {
                Name = _visitor.GetUserName(this),
                UserId = _visitor.GetUserId(this),
                Bill = createdBill
            };
            createdBill.Customers.Add(customer);

            _billsDbAccessor.AddBill(createdBill);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}")]
        public IActionResult ViewBill(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == _visitor.GetUserId(this));

            if (customer == null)
                return View("JoinBill", bill);

            var customersBill = new CustomerBillBuilder().Build(customer);

            var positions = bill.Positions
                .LeftJoin(
                    customersBill,
                    billPosition => billPosition.Id,
                    customerPosition => customerPosition.Id,
                    (billPos, customerPos) => new ViewBillPositionModel
                    {
                        Id = billPos.Id,
                        Name = billPos.Name,
                        Quantity = billPos.Quantity,
                        OriginalPrice = billPos.Price,
                        ActualPrice = customerPos?.Price ?? 0,
                        Selected = customerPos != null
                    })
                .OrderBy(positionModel => positionModel.Id)
                .ToList();

            var model = new ViewBillModel()
            {
                Bill = bill,
                Positions = positions,
                HasManageAccess = bill.UserId == _visitor.GetUserId(this),
                CustomerSum = customersBill.Sum(p => p.Price)
            };

            return View(model);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Join")]
        public IActionResult JoinBill(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
          
            return View(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}")]
        public IActionResult Delete(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);

            if (bill.UserId != _visitor.GetUserId(this))
                throw new NotImplementedException("Case is not implemented yet");

            _billsDbAccessor.DeleteById(billId);

            return RedirectToAction(nameof(Index));
        }
    }
}
