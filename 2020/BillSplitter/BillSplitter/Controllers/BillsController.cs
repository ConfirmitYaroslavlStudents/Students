using System;
using System.Collections.Generic;
using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.ViewModels.ViewBill;
using BillSplitter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
                isAdmin = b.Members.FirstOrDefault(c => c.UserId == this.GetUserId()).Role=="Admin"
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
            var member = bill.Members.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (member == null)
                return View("JoinBill", bill);

            var memberBill = new MemberBillBuilder().Build(member);

            var model = BuildVewModelForViewBill(bill, memberBill);

            return View(model);
        }

        private BillViewModel BuildVewModelForViewBill(Bill bill, List<Position> memberBill)
        {
            var positions = bill.Positions
                .Select(position => new PositionViewModel(position))
                .OrderBy(p => p.Id)
                .ToList();
           
            foreach (var pos in positions)
            {
                var memberPosition = memberBill
                    .FirstOrDefault(p => p.Id == pos.Id);
                if (memberPosition != null)
                {
                    pos.ActualPrice = memberPosition.Price;
                    pos.Selected = true;
                }
            }
            Dictionary<string, decimal> payments = new Dictionary<string, decimal>();
            foreach (var pos in memberBill)
            {
                if (!payments.ContainsKey(pos.ManagingMember.Name))
                    payments.Add(pos.ManagingMember.Name, 0);

                payments[pos.ManagingMember.Name] += pos.Price;
            }

            var member = bill.Members.FirstOrDefault(c => c.UserId == this.GetUserId());

            var model = new BillViewModel
            {
                Bill = bill,
                Positions = positions,
                Payments = payments,
                isAdmin = bill.Members.FirstOrDefault(c => c.UserId== GetUserId()).Role == "Admin",
                isModerator = member.Role == "Admin" || member.Role == "Moderator",
                MemberSum = memberBill.Sum(p => p.Price)
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
        [ValidateUserAttributeFactory(RequestedRole = "Admin")]
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
