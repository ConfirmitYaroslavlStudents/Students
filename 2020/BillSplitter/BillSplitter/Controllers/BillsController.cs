using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using BillSplitter.Controllers.Finder;
using BillSplitter.Controllers.Calculator;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillContext _context;

        public BillsController(BillContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
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

        
   
        [Authorize]
        [HttpGet]
        public IActionResult NewBill()
        {
            return View();
        }

      
        [Authorize]
        [HttpGet]
        public IActionResult SelectPositions(int id)
        {
            _context.Bill.Load();
            _context.Position.Load(); 

            var bill = _context.Bill.FirstOrDefault(e => e.Id == id);

            if (bill == null)
                return Error();
            HttpContext.Session.SetInt32("CurrentBillId", (int)id);
            return View(bill.Positions);
        }

        [Authorize]
        public IActionResult DoneSelect(int[] selected, int[] numerator, int[] denomenator, string customerName)
        {
           
            int BillId = (int)HttpContext.Session.GetInt32("CurrentBillId");
            var customer = new Customer {
                BillId = BillId,
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

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

            return RedirectToAction(nameof(CustomerBill), new { id = BillId });
        }

        [Authorize]
        public IActionResult CustomerBill(int? id)
        {
            var result = new CustomerCalculator().Calculate(_context, (int)id);
            var customer = _context.Customer.FirstOrDefault(x => x.BillId == id && x.UserId == GetCurrentUserId());

            ViewData["Sum"] = result.Item2;
            return View(result.Item1);
        }

  
        public IActionResult SummaryBill(int? id)
        {
            var currentCustomers = new CustomerFinder().Find(_context, (int)id);

            var calculator = new CustomerCalculator();

            var viewData = new List<SummaryCustomerInfo>();

            _context.Customer.Load();

            foreach(var customer in currentCustomers)
            {
                viewData.Add(new SummaryCustomerInfo { Customer = _context.Customer.Find(customer), Sum = calculator.Calculate(_context, customer).Item2});
            }

            return View(viewData);
        }
            
        
        [HttpGet]
        public IActionResult GetSummaryBill(int? id)
        {
            var billId = new BillFinder().Find(_context, (int)id).First();

            return RedirectToAction(nameof(SummaryBill), new { id = billId }); ;
        }
        
         public int GetCurrentUserId()//Возможно нужно перенести в другой класс и вызывать оттуда 
        {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        }
    }
}
