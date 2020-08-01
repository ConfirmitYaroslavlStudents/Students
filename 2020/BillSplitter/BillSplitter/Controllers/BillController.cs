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
        [Route("Home/{userId}/Bills/Index")]
        public IActionResult Index(int userId)
        {
            var userBills = _usersDbAccessor.GetUserById(userId).Bills;

            return View(userBills);
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/Create/{billName}")]
        public IActionResult Create(string billName)
        {
            var bill = new Bill(); //потом появится имя 

            _billDbAccessor.AddBill(bill);

            return RedirectToAction(nameof(Manage), new { billId = bill.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Home/{userId}/Bill/{billId}/Manage")]
        public IActionResult Manage(int userId, int billId)
        {
            var bill = _billDbAccessor.GetBillById(billId);
            return View(bill);
        }

        [Authorize]
        [HttpPost]
        [Route("Home/{userId}/Bill/{billId}/Delete")]
        public IActionResult Delete(int userId, int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _billDbAccessor.DeleteById(billId);

            return RedirectToAction(nameof(Index));
        }

        public int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        }
    }
}
