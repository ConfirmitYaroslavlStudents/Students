using System;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class BillController : Controller
    {
        private BillsDbAccessor _billDbAccessor;
        private UsersDbAccessor _usersDbAccessor;
        private UserIdVisitor _visitor;

        public BillController(BillContext context, UserIdVisitor visitor)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _visitor = visitor;
        }

        [Authorize]
        [HttpGet]
        [Route("Bills")]
        public IActionResult Index()
        {
            var userBills = _usersDbAccessor.GetUserById(_visitor.GetUserId(this)).Bills;
            ViewData["userId"] = _visitor.GetUserId(this);
            return View(userBills);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/Create")]
        public IActionResult Create()
        {
            var bill = new Bill { UserId = _visitor.GetUserId(this) };
            return View(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/Create")]
        public IActionResult Create(Bill createdBill)
        {
            createdBill.UserId = _visitor.GetUserId(this);

            _billDbAccessor.AddBill(createdBill);

            return RedirectToAction(nameof(Index), new { userId = _visitor.GetUserId(this) });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}")]
        public IActionResult ViewBill(int billId)
        {
            var bill = _billDbAccessor.GetBillById(billId);

            if (bill.UserId != _visitor.GetUserId(this))
                throw new NotImplementedException("Case is not implemented yet");

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

            return RedirectToAction(nameof(Index), new { userId = _visitor.GetUserId(this) });
        }
    }
}
