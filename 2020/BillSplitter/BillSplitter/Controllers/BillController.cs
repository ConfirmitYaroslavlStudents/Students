using System;
using System.Linq;
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

        public BillController(BillContext context)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/")]
        public IActionResult Index(int userId)
        {
            var userBills = _usersDbAccessor.GetUserById(userId).Bills;
            ViewData["userId"] = userId;
            return View(userBills);
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/Create")]
        public IActionResult Create(int userId)
        {
            var bill = new Bill { UserId = userId };
            return View(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Home/{userId}/Bill/Create")]
        public IActionResult Create(int userId, Bill createdBill)
        {
            createdBill.UserId = userId;

            _billDbAccessor.AddBill(createdBill);

            return RedirectToAction(nameof(Index), new { userId });
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/{billId}/Manage")]
        public IActionResult Manage(int userId, int billId)
        {
            var bill = _usersDbAccessor.GetUserById(userId)
                .Bills
                .FirstOrDefault(b => b.Id == billId);

            if (bill == null)
                throw new NotImplementedException("Case is not implemented yet");

            return View(bill);
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/{billId}/Delete")] // TODO Replace later with pop-up notification
        public IActionResult ConfirmDelete(int userId, int billId)
        {
            var bill = _usersDbAccessor.GetUserById(userId)
                .Bills
                .FirstOrDefault(b => b.Id == billId);

            if (bill == null)
                throw new NotImplementedException("Case is not implemented yet");

            return View(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Home/{userId}/Bill/{billId}/Delete")]
        public IActionResult Delete(int userId, int billId)
        {
            var bill = _usersDbAccessor.GetUserById(userId).Bills.FirstOrDefault(b => b.Id == billId);

            if (bill == null)
                throw new NotImplementedException("Case is not implemented yet");

            _billDbAccessor.DeleteById(billId);

            return RedirectToAction(nameof(Index), new {userId});
        }
    }
}
