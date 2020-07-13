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

        //Home/NewBill
        [HttpGet]
        public IActionResult NewBill()
        {
            return View();
        }

        //Home/SelectPositions/1
        [HttpGet]
        public IActionResult SelectPositions(int? id)
        {
            _context.Bill.Load();
            _context.Position.Load(); //возмножно не нужно

            var bill = _context.Bill.FirstOrDefault(e => e.Id == id);

            if (bill == null)
                return Error();

            return View(bill.Positions);
        }

        [HttpPost]
        public IActionResult DoneSelect(int[] selected, string customerName)
        {

            var customer = new Customer { Name = customerName };

            _context.Add(customer);
            _context.Position.Load();

            for (int i = 0; i < selected.Length; i++)
            {
                _context.Position.FirstOrDefault(x => x.Id == selected[i]).AddCustomer(customer);
            }

            _context.SaveChanges();

            stopToCheck();
            return View("Index");
        }

        private void StopToCheck()
        {
            _context.Position.Load();
            var p = _context.Position.FirstOrDefault(x => x.Id == 1);
            var p1 = _context.Position.FirstOrDefault(x => x.Id == 3);

            var k = p1.Id;
        }
    }
}
