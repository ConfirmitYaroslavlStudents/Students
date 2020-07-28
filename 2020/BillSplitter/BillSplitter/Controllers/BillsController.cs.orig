using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using BillSplitter.Models.InteractionLevel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillsDbAccessor _billHelper;
        private readonly CustomerDbAccessor _customerDbHelper;
        private readonly OrdersDbAccessor _ordersDbHelper;
        private readonly PositionsAccessor _positionsAccessor;

        public BillsController(BillContext context)
        {
            _billHelper = new BillsDbAccessor(context);
            _customerDbHelper = new CustomerDbAccessor(context);
            _ordersDbHelper = new OrdersDbAccessor(context);
            _positionsAccessor = new PositionsAccessor(context);
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
        [HttpGet] // HttpGet, but creates bill, idk how to sent post or put request via link.
        [Route("Bills/InitEmptyBill")]
        public IActionResult InitEmptyBill()
        {
            var bill = new Bill();
            _billHelper.AddBill(bill);
            return RedirectToAction(nameof(BillPositions), new { billId = bill.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/BillPositions/{billId}")]
        public IActionResult BillPositions(int billId)
        {
            ViewData["billId"] = billId;
            var positions = _billHelper.GetPositionsById(billId)
                    .Select(pos => pos.ToInteractionLevelPosition())
                    .ToList();

            return View(positions);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/AddBillPosition/{billId}")]
        public IActionResult AddBillPosition(int billId, InteractionLevelPosition position)
        {
            _positionsAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction(nameof(BillPositions), new { billId = billId});
        }
      
        [Authorize]
        [HttpGet]
        [Route("Bills/SelectPositions/{billId}")]
        public IActionResult SelectPositions(int billId)
        {
            if (_billHelper.DbContains(billId))
            {
                HttpContext.Session.SetInt32("CurrentBillId", billId);

                var positions = _billHelper.GetPositionsById(billId)
                    .Select(pos => pos.ToInteractionLevelPosition())
                    .ToList();

                return View(positions);
            }
            return Error();
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(List<InteractionLevelPosition> positions)
        {
            int billId = (int)HttpContext.Session.GetInt32("CurrentBillId"); // Remove to make stateless
            var customer = new Customer {
                BillId = billId,
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

            _customerDbHelper.AddCustomer(customer);

            _ordersDbHelper.AddOrders(customer, positions.Where(pos => pos.Selected).ToList());

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
