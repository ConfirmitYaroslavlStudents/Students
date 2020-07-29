using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using BillSplitter.Data;
using BillSplitter.Models.InteractionLevel;
using Microsoft.AspNetCore.Authorization;
using BillSplitter.Validators;

namespace BillSplitter.Controllers
{
    public class BillsController : Controller
    {
        private readonly BillsDbAccessor _billAccessor;
        private readonly CustomersDbAccessor _customerDbAccessor;
        private readonly OrdersDbAccessor _ordersDbAccessor;
        private readonly PositionsDbAccessor _positionsAccessor;

        public BillsController(BillContext context)
        {
            _billAccessor = new BillsDbAccessor(context);
            _customerDbAccessor = new CustomersDbAccessor(context);
            _ordersDbAccessor = new OrdersDbAccessor(context);
            _positionsAccessor = new PositionsDbAccessor(context);
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
            _billAccessor.AddBill(bill);
            return RedirectToAction(nameof(BillPositions), new { billId = bill.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/BillPositions/{billId}")]
        public IActionResult BillPositions(int billId)
        {
            if (!_billAccessor.DbContains(billId))
                return Error();

            ViewData["billId"] = billId;
            var positions = _billAccessor.GetBillById(billId).Positions
                .Select(pos => pos.ToInteractionLevelPosition())
                .ToList();

            return View(positions);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/AddBillPosition/{billId}")]
        public IActionResult AddBillPosition(int billId, InteractionLevelPosition position)
        {
            var validator = new Validator<InteractionLevelPosition>()
                .AddValidation(x => x.Price > 0)
                .AddValidation(x => !string.IsNullOrEmpty(x.Name.Trim()))
                .AddValidation(x => x.QuantityDenomenator == 1)
                .AddValidation(x => x.QuantityNumerator > 0);

            if (!validator.Validate(position))
                return Error();

            _positionsAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction(nameof(BillPositions), new {billId = billId});
        }
      
        [Authorize]
        [HttpGet]
        [Route("Bills/SelectPositions/{billId}")]
        public IActionResult SelectPositions(int billId)
        {
            if (!_billAccessor.DbContains(billId))
                return Error();
            ViewData["billId"] = billId;

            var positions = _billAccessor.GetBillById(billId).Positions
                .Select(pos => pos.ToInteractionLevelPosition())
                .ToList();

            return View(positions);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(int billId, List<InteractionLevelPosition> positions)
        {
            if (!_billAccessor.DbContains(billId))
                return Error();

            var customer = new Customer {
                BillId = billId,
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

            var selectedPositions = positions.Where(pos => pos.Selected).ToList();

            var validator = new Validator<InteractionLevelPosition>()
                .AddValidation(x => _positionsAccessor.DbContains(x.Id))
                .AddValidation(x => x.QuantityDenomenator > 0)
                .AddValidation(x => x.QuantityNumerator > 0);

            if (selectedPositions.All(validator.Validate))
            {
                _customerDbAccessor.AddCustomer(customer);

                _ordersDbAccessor.AddOrders(customer, selectedPositions);

                return RedirectToAction(nameof(CustomerBill), new {customerId = customer.Id});
            }

            return Error();
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/CustomerBill/{customerId}")]
        public IActionResult CustomerBill(int customerId)
        {
            if (!_customerDbAccessor.DbContains(customerId))
                return Error();

            var result = new CustomerBillBuilder()
                .Build(_customerDbAccessor.GetCustomerById(customerId));

            ViewData["Sum"] = result.Sum(x => x.Price);
            ViewData["customerId"] = customerId;
            return View(result);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/SummaryBill/{billId}")]
        public IActionResult SummaryBill(int billId)
        {
            if (!_billAccessor.DbContains(billId))
                return Error();

            var currentCustomers = _billAccessor.GetBillById(billId).Customers;

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
            if (!_customerDbAccessor.DbContains(customerId))
                return Error();

            var customerBillId = _customerDbAccessor.GetCustomerById(customerId).BillId;

            return RedirectToAction(nameof(SummaryBill), new { billId = customerBillId });
        }
        
         public int GetCurrentUserId()
         {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
         }
    }
}
