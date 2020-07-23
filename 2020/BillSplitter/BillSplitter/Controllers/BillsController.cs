using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult NewBill()
        {
            return View();
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

            return $"SelectPositions?billId={bill.Id}";
        }
      
        [Authorize]
        [HttpGet]
        public IActionResult SelectPositions(int billId)
        {
            _context.Bill.Load();
            _context.Position.Load(); 

            var bill = _context.Bill.FirstOrDefault(e => e.Id == billId);

            if (bill == null)
                return Error();
            HttpContext.Session.SetInt32("CurrentBillId", (int)billId);
            return View(bill.Positions);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(int[] selected, int[] numerator, int[] denomenator, string customerName)
        {
            int billId = (int)HttpContext.Session.GetInt32("CurrentBillId"); // Remove to make stateless
            var customer = new Customer {
                BillId = billId,
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

            return RedirectToAction(nameof(CustomerBill), new { customerId = customer.Id });
        }

        [Authorize]
        [HttpGet]
        public IActionResult CustomerBill(int? customerId)
        {
            var result = new CustomerCalculator().Calculate(_context, (int)customerId);

            // Unused code
            var customer = _context.Customer.FirstOrDefault(x => x.BillId == customerId && x.UserId == GetCurrentUserId());

            ViewData["Sum"] = result.Item2;
            ViewData["customerId"] = customerId;
            return View(result.Item1);
        }

        // Authorize?
        [HttpGet]
        public IActionResult SummaryBill(int? billId)
        {
            var currentCustomers = new CustomerFinder().Find(_context, (int)billId);

            var calculator = new CustomerCalculator();

            var customerInfos = new List<SummaryCustomerInfo>();

            _context.Customer.Load();

            foreach(var customer in currentCustomers)
            {
                customerInfos.Add(new SummaryCustomerInfo
                {
                    Customer = _context.Customer.Find(customer), 
                    Sum = calculator.Calculate(_context, customer).Item2
                });
            }

            return View(customerInfos);
        }

        // Authorize?
        [HttpGet]
        public IActionResult GetSummaryBill(int? customerId)
        {
            var customerBillId = new BillFinder().Find(_context, (int)customerId).First();

            return RedirectToAction(nameof(SummaryBill), new { billId = customerBillId }); ;
        }
        
         public int GetCurrentUserId()
         {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
         }
    }
}
