using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BillSplitter.Data;

namespace BillSplitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BillContext _context;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(BillContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public string AddNewBill([FromBody] Position[] positions)
        {
            ViewData["positions"] = positions;
            //Нужно теперь сделать новый Bill с этими позициями и запихать его в базу данных 

            var positionsList = new List<Position>(positions);

            var bill = new Bill {
                Positions = positionsList
            };

            _context.Add(bill);
            _context.SaveChanges();

            return "Home/Bills/SelectPositions";
        }

        //Home/NewBill
        [HttpGet]
        public async Task<IActionResult> NewBill()
        {
            return View();
        }

    }
}
