using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class SuperController : Controller
    {

        protected readonly BillsDbAccessor _billsDbAccessor;
        protected readonly UsersDbAccessor _usersDbAccessor;
        protected readonly CustomersDbAccessor _customersDbAccessor;
        protected readonly OrdersDbAccessor _ordersDbAccessor;
        protected readonly PositionsDbAccessor _positionsDbAccessor;
        public SuperController(BillContext context)
        {
            _billsDbAccessor = new BillsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _customersDbAccessor = new CustomersDbAccessor(context);
            _ordersDbAccessor = new OrdersDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
            
        }

        public int GetUserId()
        {
            return int.Parse(
                    HttpContext
                    .User.Claims
                    .Where(c => c.Type == "Id")
                    .Select(c => c.Value)
                    .SingleOrDefault() ?? string.Empty);
        }

        public string GetUserName()
        {
            return User.Identity.Name;
        }

        public bool CheckUserAccessToBill(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill == null) return false;
           return bill.UserId == GetUserId();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
