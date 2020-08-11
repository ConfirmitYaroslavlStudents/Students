using System.Diagnostics;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class SuperController : Controller // DataController or something, but NOT SuperController
    {
        protected readonly UnitOfWork Uow;

        public SuperController(BillContext context)
        {
            Uow = new UnitOfWork(context);

        }

        public int GetUserId() // Maybe move to extension method? This provides http context data...
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

        public Bill GetBillById(int billId) // ...and this provides db context data
        {
            return Uow.Bills.GetBillById(billId); 
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
