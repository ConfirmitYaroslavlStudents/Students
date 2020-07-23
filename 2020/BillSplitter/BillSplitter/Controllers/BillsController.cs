using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillsDbHelper _billHelper;
        private readonly CustomerDbHelper _customerDbHelper;
        private readonly OrdersDbHelper _ordersDbHelper;

        public BillsController(BillContext context)
        {
            _billHelper = new BillsDbHelper(context);
            _customerDbHelper = new CustomerDbHelper(context);
            _ordersDbHelper = new OrdersDbHelper(context);
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
            
            // learn how to bind in array
            var positionsList = new List<Position>();
            for (int i = 0; i < name.Length; i++)
                positionsList.Add(new Position { Name = name[i], Price = price[i], Quantity = quantity[i] });

            var bill = new Bill {
                Positions = positionsList
            };

            _billHelper.AddBill(bill);

            return RedirectToAction(nameof(SelectPositions), new { billId = bill.Id });
        }
      
        [Authorize]
        [HttpGet]
        [Route("Bills/SelectPositions/{billId}")]
        public IActionResult SelectPositions(int billId)
        {
            if (_billHelper.DbContains(billId))
            {
                HttpContext.Session.SetInt32("CurrentBillId", billId);

                return View(_billHelper.GetPositionsById(billId));
            }
            return Error();
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(int[] selected, int[] numerator, int[] denomenator)
        {
            int billId = (int)HttpContext.Session.GetInt32("CurrentBillId"); // Remove to make stateless
            var customer = new Customer {
                BillId = billId,
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

            _customerDbHelper.AddCustomer(customer);

            _ordersDbHelper.AddOrders(customer, selected, numerator, denomenator);

            return RedirectToAction(nameof(CustomerBill), new { customerId = customer.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/CustomerBill/{customerId}")]
        public IActionResult CustomerBill(int customerId)
        {
            var result = new CustomerBillBuilder().Build(_customerDbHelper.GetCustomerById(customerId));

            ViewData["Sum"] = result.Sum(x => x.Price);
            ViewData["customerId"] = customerId;
            return View(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/SummaryBill/{billId}")]
        public IActionResult SummaryBill(int billId)
        {
            var currentCustomers = _billHelper.GetCustomersById(billId);

            var builder = new CustomerBillBuilder();

            var customerInfos = new List<SummaryCustomerInfo>();

            foreach(var customer in currentCustomers)
            {
                customerInfos.Add(new SummaryCustomerInfo
                {
                    Customer = customer, 
                    Sum = builder.Build(customer).Sum(x => x.Price)
                });
            }

            return View(customerInfos);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/GetSummaryBill/{customerId}")]
        public IActionResult GetSummaryBill(int customerId)
        {
            var customerBillId = _customerDbHelper.GetCustomerById(customerId).BillId;

            return RedirectToAction(nameof(SummaryBill), new { billId = customerBillId });
        }
        
         public int GetCurrentUserId()
         {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
         }
    }

}
