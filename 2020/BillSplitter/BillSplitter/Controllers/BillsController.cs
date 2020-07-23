using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
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
        public IActionResult NewBill(string[] name, decimal[] price, int[] quantity)
        {
            //validate
            
            var positionsList = new List<Position>();
            for (int i = 0; i < name.Length; i++)
                positionsList.Add(new Position { Name = name[i], Price = price[i], Quantity = quantity[i] });

            var bill = new Bill {
                Positions = positionsList
            };

            _context.Add(bill);
            _context.SaveChanges();

            return RedirectToAction(nameof(SelectPositions), new { billId = bill.Id });
        }
      
        [Authorize]
        [HttpGet]
        [Route("Bills/SelectPositions/{billId}")]
        public IActionResult SelectPositions(int billId)
        {
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
        [Route("Bills/CustomerBill/{customerId}")]
        public IActionResult CustomerBill(int customerId)
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
        [Route("Bills/SummaryBill/{billId}")]
        public IActionResult SummaryBill(int billId)
        {
            var currentCustomers = new CustomerFinder().Find(_context, (int)billId);

            var calculator = new CustomerCalculator();

            var customerInfos = new List<SummaryCustomerInfo>();

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
        [Route("Bills/GetSummaryBill/{customerId}")]
        public IActionResult GetSummaryBill(int customerId)
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
