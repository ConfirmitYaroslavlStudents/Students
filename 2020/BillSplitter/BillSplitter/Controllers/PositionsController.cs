﻿using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BillSplitter.Validators;

namespace BillSplitter.Controllers
{
    public class PositionsController : SuperController
    {
       

        public PositionsController(BillContext context) : base(context)
        {
           
        }

        private bool ValidateUser(int billId)
        {
            var bill = _billsDbAccessor.GetBillById(billId);
            if (bill == null) 
                return false;
            if (bill.UserId != this.GetUserId())
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

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/PickPositions")]
        public IActionResult PickPositions(int billId)
        {
            ViewData["billId"] = billId;

            var bill = _billsDbAccessor.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (customer != null)
                return View(bill.Positions.OrderBy(p => p.Id).ToList());

            return RedirectToAction("JoinBill", "Bills", new { billId });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/ManagePositions")]
        [ValidateUser]
        public IActionResult ManagePositions(int billId)
        {
            ViewData["billId"] = billId;


            var positions = _billsDbAccessor.GetBillById(billId).Positions
                .OrderBy(p => p.Id)
                .ToList();

            return View(positions);
        }


        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions")]
        [ValidateUser]
        public IActionResult Create(int billId, Position position)
        {

            _positionsDbAccessor.AddPosition(position);

            return RedirectToAction(nameof(ManagePositions), new { billId }); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

            _positionsDbAccessor.DeleteById(positionId);

            return RedirectToAction(nameof(ManagePositions), new { billId, });
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Update")] // How to implement RESTful?
        [ValidateUser]
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!ValidatePosition(billId, positionId))
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
