using System.Diagnostics;
using System.Globalization;
using System.Threading;
using BillSplitter.Data;
using BillSplitter.HttpContextExtension;
using BillSplitter.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UnitOfWork Db;

        public BaseController(UnitOfWork db)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");

            Db = db;
        }

        public int GetUserId() 
        {
            return HttpContext.GetCurrentUserId();
        }

        public string GetUserName()
        {
            return User.Identity.Name;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
