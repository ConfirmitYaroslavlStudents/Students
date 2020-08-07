using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class SuperController : Controller
    {
       protected readonly UnitOfWork _uow;

        public SuperController(BillContext context)
        {
            _uow = new UnitOfWork(context);

        }

        public int GetUserId()
        {
            return int.Parse(
                    HttpContext
                    .User.Claims
                    .Where(c => c.Type == "Id")
                    .Select(c => c.Value)
                    .SingleOrDefault() ?? string.Empty);
        }

        public string GetUserName()
        {
            return User.Identity.Name;
        }

        public bool CheckUserAccessToBill(int billId)
        {
            var bill = _uow.Bills.GetBillById(billId);
            if (bill == null) return false;
           return bill.UserId == GetUserId();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
