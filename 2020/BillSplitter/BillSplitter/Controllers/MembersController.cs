using System.Linq;
using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Members")]
    public class MembersController : BaseController
    {
        public MembersController(UnitOfWork db) : base(db)
        {

        }
        
        [HttpGet]
        [ValidateUserAttributeFactory(RequestedRole = "Admin")]
        public IActionResult Index(int billId)
        {
            var bill = Db.Bills.GetBillById(billId);

            ViewData["billId"] = billId;
            var members = bill.Members;

            return View(members);
        }

        [HttpPost]
        public IActionResult AddMemberOrContinueToPick(int billId) 
        {
            if (!Db.Bills.Exist(billId))
                Error();
            
            var userId = GetUserId();
            var bill = Db.Bills.GetBillById(billId);
            
            if (bill.Members.FirstOrDefault(c => c.UserId == userId) == null)
            {
                var member = new Member
                {
                    BillId = billId,
                    UserId = GetUserId(),
                    Role = "Guest",
                    Name = GetUserName()
                };

                Db.Members.Add(member);
                Db.Save();
            }

            return RedirectToAction("PickPositions", "Positions", new {billId});
        }

        [HttpPost]
        [Route("{memberId}/UpdateRole")]
        [RequireRoles("Admin")]
        public IActionResult UpdateRole(int billId, int memberId, string role)
        {
            Db.Members.UpdateMemberRoleById(memberId, role);
            Db.Save();

            return RedirectToAction(nameof(Index), new {billId = billId});
        }

        [HttpPost]
        [Route("{memberId}")]
        [RequireRoles("Admin")]
        public IActionResult Delete(int billId, int memberId) // TODO Maybe delete confirmation?
        {
            Db.Members.DeleteById(memberId);
            Db.Save();
            return RedirectToAction(nameof(Index), new { billId = billId });
        }
    }
}
