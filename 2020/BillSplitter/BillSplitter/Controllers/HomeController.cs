﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BillSplitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly BillContext _context;

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

            var positionsList = new List<Position>(positions);

            var bill = new Bill {
                Positions = positionsList
            };

            _context.Add(bill);
            _context.SaveChanges();

            return $"SelectPositions/{bill.Id}";
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
            var customer = new Customer { Name = customerName, Positions = new List<Position>() };

            _context.Position.Load();

            for (int i = 0; i < selected.Length; i++)
                customer.Positions.Add(_context.Position.FirstOrDefault(x => x.Id == selected[i]));

            _context.Customer.Add(customer);
            _context.SaveChanges();

            return RedirectToAction(nameof(CustomerBill), new { id = customer.Id });
        }

        [HttpGet]
        public IActionResult CustomerBill(int? id)
        {
            _context.Customer.Load();
            _context.Position.Load();
            var customer = _context.Customer.FirstOrDefault(x => x.Id == id);

            var sum = customer.Positions.Select(x => x.Price).Sum();

            ViewData["Sum"] = sum;
            return View(customer.Positions);
        }
    }
}
