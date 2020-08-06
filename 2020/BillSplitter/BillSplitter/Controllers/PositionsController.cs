using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BillSplitter.Controllers
{
    public class PositionsController : Controller 
    {
        private readonly BillsDbAccessor _billsDbAccessor;
        private readonly PositionsDbAccessor _positionsDbAccessor;
        private readonly UserIdVisitor _visitor;
        public PositionsController(BillContext context, UserIdVisitor userIdVisitor)
        {
            _billsDbAccessor = new BillsDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
            _visitor = userIdVisitor;
        }

        private bool ValidateUser(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill == null) 
                return false;
            if (bill.UserId != _visitor.GetUserId(this))
                return false;

            return true;
        }

        private bool ValidatePosition(int billId, int positionId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);

            if (bill.Positions.Find(x => x.Id == positionId) == null)
                return false;

            return true;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/PickPositions")]
        public IActionResult PickPositions(int billId)
        {
            ViewData["billId"] = billId;

            var bill = _billsDbAccessor.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == _visitor.GetUserId(this));

            if (customer != null)
                return View(bill.Positions.OrderBy(p => p.Id).ToList());

            return RedirectToAction("JoinBill", "Bills", new { billId });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/ManagePositions")]
        public IActionResult ManagePositions(int billId)
        {
            ViewData["billId"] = billId;

            if (!ValidateUser(billId))
                return Error();

            var positions = _billsDbAccessor.GetBillById(billId).Positions
                .OrderBy(p => p.Id)
                .ToList();

            return View(positions);
        }


        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions")]
        public IActionResult Create(int billId, Position position)
        {
            if (!ValidateUser(billId))
                return Error();

            _positionsDbAccessor.AddPosition(position);

            return RedirectToAction(nameof(ManagePositions), new { billId }); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}")]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!ValidateUser(billId) || !ValidatePosition(billId, positionId))
                return Error();

            _positionsDbAccessor.DeleteById(positionId);

            return RedirectToAction(nameof(ManagePositions), new { billId, });
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Update")] // How to implement RESTful?
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!ValidateUser(billId) || !ValidatePosition(billId, positionId))
                return Error();

            _positionsDbAccessor.UpdateById(positionId, position);
            return RedirectToAction(nameof(ManagePositions), new { billId });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Positions/{positionId}/Partial")] 
        public PartialViewResult GetPositionPartial(int billId, int positionId)
        {
            Position position = _positionsDbAccessor.GetPositionById(positionId);
            return new PartialViewResult
            {
                ViewName = "SelectPositionsPartial",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = position
                }
            };
        }
    }
}
