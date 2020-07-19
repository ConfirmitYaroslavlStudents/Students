using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BillSplitter.Controllers.Finder;
using BillSplitter.Controllers.Calculator;
using System;

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

        [HttpPost] // /Home/SelectPositions/5
        public IActionResult DoneSelect(int[] selected, int[] numerator, int[] denomenator, string customerName)
        {
            var customer = new Customer { Name = customerName };

            _context.Position.Load();

            _context.Customer.Add(customer);
            _context.SaveChanges();

            for (int i = 0; i < selected.Length; i++)
            {
                if (1.0 * numerator[i] / denomenator[i] > double.Epsilon)
                {
                    var order = new Order
                    {
                        CustomerId = customer.Id,
                        PositionId = selected[i],
                        Quantity = 1.0 * numerator[i] / denomenator[i]
                    };

                    _context.Orders.Add(order);
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(CustomerBill), new { id = customer.Id });
        }

        [HttpGet]
        public IActionResult CustomerBill(int? id)
        {
            var result = new CustomerCalculator().Calculate(_context, (int)id);

            ViewData["Sum"] = result.Item2;
            return View(result.Item1);
        }
        [HttpGet]
        public IActionResult SummaryBill(int? billId)
        {
            if (billId == null)
                return Error();

            var currentCustomers = new CustomerFinder().Find(_context, (int)billId);

            var calculator = new CustomerCalculator();

            var viewData = new List<Tuple<string, decimal, int>>();

            foreach(var customer in currentCustomers)
            {
                viewData.Add(new Tuple<string, decimal, int>(customer.Name, calculator.Calculate(_context, customer.Id).Item2, customer.Id));
            }

            return View(viewData);
        }
    }
}
