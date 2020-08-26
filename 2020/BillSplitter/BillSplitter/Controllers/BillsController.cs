using System;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels.ViewBill;
using BillSplitter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Calculators;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills")]
    public class BillsController : BaseController
    {
        public BillsController(UnitOfWork db) : base(db)
        {
           
        }

        [HttpGet]
        [Route("~/")]
        public IActionResult Index()
        {

            List<BillViewModel> bills = Db.Bills.GetByMemberUserId(GetUserId()).Select(b => new BillViewModel()
            {
                Bill = b,
                isAdmin = b.Members.FirstOrDefault(c => c.UserId == GetUserId()).Role=="Admin"
            }).ToList();

            return View(bills);
        }

        [HttpPost]
        public IActionResult Create(Bill createdBill)
        {
            createdBill.UserId = GetUserId();
            var member = new Member
            {
                Name = GetUserName(),
                UserId = GetUserId(),
                Role = "Admin",
                Bill = createdBill
            };

            Db.Bills.Add(createdBill);
            Db.Members.Add(member);

            Db.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{billId}")]
        public IActionResult ViewBill(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);
            var member = bill.Members.FirstOrDefault(c => c.UserId == GetUserId());

            if (member == null)
                return View("JoinBill", bill);

            var model = BuildVewModelForViewBill(
                bill, 
                member, 
                new MemberBillBuilder(new OrderPriceCalculator()));

            return View(model);
        }

        private BillViewModel BuildVewModelForViewBill(Bill bill, Member billMember, MemberBillBuilder billBuilder)
        {
            var memberBill = billBuilder.Build(billMember);
            var member = bill.Members.FirstOrDefault(c => c.UserId == GetUserId());

            var balanceCalculator = new BalanceCalculator(billBuilder.Calculator);

            var debts = balanceCalculator.CalculateDebts(member);
            var payments = balanceCalculator.CalculatePayments(member);

            var billPositions = bill.Positions
                .Select(position => new PositionViewModel(position))
                .OrderBy(p => p.Id)
                .ToList();

            foreach (var pos in billPositions)
            {
                var memberPosition = memberBill
                    .FirstOrDefault(p => p.Id == pos.Id);

                if (memberPosition != null)
                {
                    pos.ActualPrice = memberPosition.Price;
                    pos.Selected = true;
                }
            }

            var model = new BillViewModel
            {
                Bill = bill,
                Positions = billPositions,
                Payments = payments,
                Debts = debts,
                isAdmin = bill.Members.FirstOrDefault(c => c.UserId == GetUserId()).Role == "Admin",
                isModerator = member.Role == "Admin" || member.Role == "Moderator",
                MemberSum = payments.Values.Sum()
            };

            return model;
        }

        [HttpGet]
        [Route("{billId}/Join")]
        public IActionResult JoinBill(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);
          
            return View(bill);
        }

        [HttpPost]
        [Route("{billId}")]
        [RequireRoles("Admin")]
        public IActionResult Delete(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);

            if (bill.UserId != GetUserId())
                throw new NotImplementedException("Case is not implemented yet");

            Db.Bills.DeleteById(billId);
            Db.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
