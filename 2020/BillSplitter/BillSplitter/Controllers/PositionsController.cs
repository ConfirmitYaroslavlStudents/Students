﻿using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BillSplitter.Attributes;
using System.Collections.Generic;
using BillSplitter.Models.ViewModels.PickPositions;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Positions")]
    public class PositionsController : BaseController
    {
        public PositionsController(UnitOfWork db) : base(db)
        {
           
        }

        private bool ValidatePosition(int billId, int positionId) // Move to attribute
        {
            var bill = Db.Bills.GetBillById(billId);

            if (bill.Positions.Find(x => x.Id == positionId) == null)
                return false;

            return true;
        }

        [HttpGet]
        [Route("Pick")]
        public IActionResult PickPositions(int billId)
        {
            ViewData["billId"] = billId;

            var bill = Db.Bills.GetBillById(billId);
            var member = bill.Members.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (member != null)
            {
                var positions = bill.Positions.OrderBy(p => p.Id).ToList();
                var model = new List<PositionViewModel>();

                foreach (var position in positions)
                    model.Add(GetPositionViewModel(position));

                return View(model);
            }
            return RedirectToAction("JoinBill", "Bills", new { billId });
        }

        private PositionViewModel GetPositionViewModel(Position position)
        {
            var order = position.Orders.Find(x => x.Member.UserId == GetUserId());
            return new PositionViewModel(position, order);
        }

        [HttpGet]
        [Route("Manage")]
        [RequireRoles("Admin, Moderator")]
        public IActionResult ManagePositions(int billId)
        {
            ViewData["billId"] = billId;

            var positions = Db.Bills.GetBillById(billId).Positions
                .OrderBy(p => p.Id)
                .ToList();

            return View(positions);
        }

        [HttpPost]
        [RequireRoles("Admin, Moderator")]
        public IActionResult Create(int billId, Position position)
        {
            position.ManagingMemberId = Db.Bills.GetBillById(billId).Members.FirstOrDefault(c =>  c.UserId == GetUserId()).Id;

            Db.Positions.Add(position);
            
            Db.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId }); 
        }

        [HttpPost]
        [Route("{positionId}")]
        [RequireRoles("Admin, Moderator")]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

            Db.Positions.DeleteById(positionId);

            Db.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId, });
        }

        [HttpPost]
        [Route("{positionId}/Update")]
        [RequireRoles("Admin, Moderator")]
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

            Db.Positions.UpdateById(positionId, position);

            Db.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId });
        }

        [HttpGet]
        [Route("{positionId}/Partial")] 
        public PartialViewResult GetPositionPartial(int billId, int positionId)
        {
            Position position =  Db.Positions.GetById(positionId);
            return new PartialViewResult
            {
                ViewName = "PickPositionsPartial",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = GetPositionViewModel(position)
                }
            };
        }


        [HttpGet]
        [Route("{positionId}/DialogPartial")]
        public PartialViewResult GetPositionDialogPartial(int billId, int positionId)
        {
            Position position = Db.Positions.GetById(positionId);
            return new PartialViewResult
            {
                ViewName = "EnterValueDialogPartial",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = GetPositionViewModel(position)
                }
            };
        }
    }
}
