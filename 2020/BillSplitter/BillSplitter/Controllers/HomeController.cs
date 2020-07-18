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

        
        //Home/NewBill
        [Authorize]
        [HttpGet]
        public IActionResult NewBill()
        {
            return View();
        }

        //Home/SelectPositions/1
        [Authorize]
        [HttpGet]
        public IActionResult SelectPositions(int? id)//bill_id
        {
            _context.Bill.Load();
            _context.Position.Load(); //возмножно не нужно

            var bill = _context.Bill.FirstOrDefault(e => e.Id == id);

            if (bill == null)
                return Error();
            HttpContext.Session.SetInt32("CurrentBillId", (int)id);
            int BillId = (int)HttpContext.Session.GetInt32("CurrentBillId");
            return View(bill.Positions);
        }

        [Authorize]
        [HttpPost]
        public IActionResult DoneSelect(int[] selected)
        {
           
            int BillId = (int)HttpContext.Session.GetInt32("CurrentBillId");
            var customer = new Customer {
                BillId = BillId,
                UserId = GetCurrentUserId()
            };

            _context.Position.Load();

            _context.Customer.Add(customer);
            _context.SaveChanges();

            for (int i = 0; i < selected.Length; i++)
            {
                var order = new Order { CustomerId = customer.Id, PositionId = selected[i] };
                _context.Orders.Add(order);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(CustomerBill), new { id = BillId });
        }

        [Authorize]
        [HttpGet]
        public IActionResult CustomerBill(int? id)//bill_id
        {
            _context.Customer.Load();
            _context.Position.Load();
            _context.Orders.Load();

            var customer = _context.Customer.FirstOrDefault(x => x.BillId == id && x.UserId == GetCurrentUserId());
            var positions = new List<Position>();

            var sum = 0m;
            foreach (var order in customer.Orders)
            {
                var posId = order.PositionId;
                var position = _context.Position.FirstOrDefault(x => x.Id == posId);

                var customerPrice = position.Price / position.Orders.Count;
                positions.Add(new Position { Name = position.Name, Price =  customerPrice});

                sum += customerPrice;
            }

            ViewData["Sum"] = sum;
            return View(positions);
        }

        public int GetCurrentUserId()
        {
            return int.Parse(HttpContext.User.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
        }
    }
}
