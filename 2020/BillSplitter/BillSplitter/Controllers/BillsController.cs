using System;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillsDbAccessor _billDbAccessor;
        private readonly UsersDbAccessor _usersDbAccessor;
        private readonly UserIdVisitor _visitor;

        public BillsController(BillContext context, UserIdVisitor visitor)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var userBills = _usersDbAccessor.GetUserById(_visitor.GetUserId(this)).Bills;
            ViewData["userId"] = _visitor.GetUserId(this);
            return View(userBills);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/")]
        public IActionResult Create(Bill createdBill)
        {
            createdBill.UserId = _visitor.GetUserId(this);

            _billDbAccessor.AddBill(createdBill);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}")]
        public IActionResult ViewBill(int billId)
        {
            var bill = _billDbAccessor.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == _visitor.GetUserId(this));

            var customersBill = new CustomerBillBuilder().Build(customer);

            var postions =
                (
                    from bPos in bill.Positions
                    join cPos in customersBill on bPos.Id equals cPos.Id
                        into bcPositions
                    from pos in bcPositions.DefaultIfEmpty()
                    select new ViewBillPositionModel()
                    {
                        Name = bPos.Name, 
                        Quantity = bPos.Quantity,
                        OriginalPrce = bPos.Price,
                        ActualPrice = pos?.Price ?? 0,
                        Selected = pos != null
                    }
                ).ToList();

            var model = new ViewBillModel()
            {
                Bill = bill,
                Positions = postions,
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
            var bill = _billDbAccessor.GetBillById(billId);
          
            return View(bill);
        }


        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}")]
        public IActionResult Delete(int billId)
        {
            var bill = _billDbAccessor.GetBillById(billId);

            if (bill.UserId != _visitor.GetUserId(this))
                throw new NotImplementedException("Case is not implemented yet");

            _billDbAccessor.DeleteById(billId);

            return RedirectToAction(nameof(Index));
        }
    }
}
