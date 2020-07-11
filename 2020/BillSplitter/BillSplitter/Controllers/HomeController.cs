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
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

            return $"Home/SelectPositions/{bill.Id}";
        }

        public string GetPositions(int? id)
        {
            // пример того, как надо работать с данными в EF
            // заменить на то, что нужно
            _context.Bill.Load();
            _context.Position.Load();

            var bills = _context.Bill.FirstOrDefault(x => x.Id == id);
            return bills.Positions.Count().ToString();
        }

        //Home/NewBill
        [HttpGet]
        public async Task<IActionResult> NewBill()
        {
            return View();
        }

        //Home/SelectPositions/1
        [HttpGet]
        public async Task<IActionResult> SelectPositions(int? id)
        {
            _context.Bill.Load();
            _context.Position.Load(); //возмножно не нужно

            var bill = _context.Bill.FirstOrDefault(e => e.Id == id);

            if (bill == null)
                return Error();

            return View(bill.Positions);
        }
        [HttpPost]
        public async Task<IActionResult> DoneSelect([FromBody] ReservedPosition[] selected)
        {
            selected.Count();
    
            return View("Index");
        }
    }
}
