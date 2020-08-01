using System;
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
    [Obsolete]
    public class BillsController : Controller
    {
        private BillsDbAccessor _billDbAccessor;
        private CustomersDbAccessor _customerDbAccessor;
        private OrdersDbAccessor _ordersDbAccessor;
        private PositionsDbAccessor _positionsDbAccessor;

        private IValidator<InteractionLevelPosition> _addBillPositionValidator;
        private IValidator<InteractionLevelPosition> _doneSelectValidator;

        public BillsController(BillContext context)
        {
            InitializeAccessors(context);

            InitializeValidators();
        }

        private void InitializeAccessors(BillContext context)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _customerDbAccessor = new CustomersDbAccessor(context);
            _ordersDbAccessor = new OrdersDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
        }

        private void InitializeValidators()
        {
            _addBillPositionValidator = new Validator<InteractionLevelPosition>()
                .AddValidation(x => x.Price > 0)
                .AddValidation(x => !string.IsNullOrEmpty(x.Name.Trim()))
                .AddValidation(x => x.QuantityDenomenator == 1)
                .AddValidation(x => x.QuantityNumerator > 0);

            _doneSelectValidator = new Validator<InteractionLevelPosition>()
                .AddValidation(x => _positionsDbAccessor.DbContains(x.Id))
                .AddValidation(x => x.QuantityDenomenator > 0)
                .AddValidation(x => x.QuantityNumerator > 0);
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
            _billDbAccessor.AddBill(bill);
            return RedirectToAction(nameof(BillPositions), new { billId = bill.Id });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/BillPositions/{billId}")]
        public IActionResult BillPositions(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                return Error();

            ViewData["billId"] = billId;
            var positions = _billDbAccessor.GetBillById(billId).Positions
                .Select(pos => pos.ToInteractionLevelPosition())
                .ToList();

            return View(positions);
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/AddBillPosition/{billId}")]
        public IActionResult AddBillPosition(int billId, InteractionLevelPosition position)
        {
            if (!_addBillPositionValidator.Validate(position))
                return Error();

            _positionsDbAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction(nameof(BillPositions), new {billId = billId});
        }
      
        [Authorize]
        [HttpGet]
        [Route("Bills/SelectPositions/{billId}")]
        public IActionResult SelectPositions(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                return Error();
            ViewData["billId"] = billId;

            var positions = _billDbAccessor.GetBillById(billId).Positions
                .Select(pos => pos.ToInteractionLevelPosition())
                .ToList();

            return View(positions);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(int billId, List<InteractionLevelPosition> positions)
        {
            if (!_billDbAccessor.DbContains(billId))
                return Error();

            var customer = new Customer {
                BillId = billId,
                UserId = GetCurrentUserId(),
                Name = HttpContext.User.Identity.Name
            };

            var selectedPositions = positions.Where(pos => pos.Selected).ToList();

            if (selectedPositions.All(_doneSelectValidator.Validate))
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
            if (!_billDbAccessor.DbContains(billId))
                return Error();

            var currentCustomers = _billDbAccessor.GetBillById(billId).Customers;

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
