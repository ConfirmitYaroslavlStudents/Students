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

        public Bill GetBillById(int billId)
        {
            return _uow.Bills.GetBillById(billId); 
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
