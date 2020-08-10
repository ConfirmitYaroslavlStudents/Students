using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BillSplitter.Validators;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Positions")]
    public class PositionsController : SuperController
    {
        public PositionsController(BillContext context) : base(context)
        {
           
        }

        private bool ValidatePosition(int billId, int positionId)
        {
            var bill = _uow.Bills.GetBillById(billId);

            if (bill.Positions.Find(x => x.Id == positionId) == null)
                return false;

            return true;
        }

        
        [HttpGet]
        [Route("Pick")]
        public IActionResult PickPositions(int billId)
        {
            ViewData["billId"] = billId;

            var bill = _uow.Bills.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (customer != null)
                return View(bill.Positions.OrderBy(p => p.Id).ToList());

            return RedirectToAction("JoinBill", "Bills", new { billId });
        }

        [HttpGet]
        [Route("Manage")]
        [ValidateUser]
        public IActionResult ManagePositions(int billId)
        {
            ViewData["billId"] = billId;


            var positions = _uow.Bills.GetBillById(billId).Positions
                .OrderBy(p => p.Id)
                .ToList();

            return View(positions);
        }

        [HttpPost]
        [ValidateUser]
        public IActionResult Create(int billId, Position position)
        {

             _uow.Positions.Add(position);
            _uow.Save();
            return RedirectToAction(nameof(ManagePositions), new { billId }); 
        }

        [HttpPost]
        [Route("{positionId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

             _uow.Positions.DeleteById(positionId);
            _uow.Save();
            return RedirectToAction(nameof(ManagePositions), new { billId, });
        }

        [HttpPost]
        [Route("{positionId}/Update")] // How to implement RESTful?
        [ValidateUser]
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

             _uow.Positions.UpdateById(positionId, position);
            _uow.Save();
            return RedirectToAction(nameof(ManagePositions), new { billId });
        }

        [Authorize]
        [HttpGet]
        [Route("{positionId}/Partial")] 
        public PartialViewResult GetPositionPartial(int billId, int positionId)
        {
            Position position =  _uow.Positions.GetById(positionId);
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
