using BillSplitter.Data;
using BillSplitter.Models.InteractionLevel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BillSplitter.Controllers
{
    public class PositionsController : Controller
    {
        private BillsDbAccessor _billDbAccessor;
        private PositionsDbAccessor _positionsDbAccessor;

        public PositionsController(BillContext context, UserIdVisitor userIdVisitor)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Positions")]
        public IActionResult Index(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            ViewData["billId"] = billId;
            var positions = _billDbAccessor.GetBillById(billId).Positions;

            return View(positions); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions")]
        public IActionResult Create(int billId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction(nameof(Index), new { billId }); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Delete")]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.DeleteById(positionId);

            return RedirectToAction(nameof(Index), new { billId });
        }

        [Authorize]
        [HttpGet]
        [Route("Bills/{billId}/Positions/{positionId}")]
        public IActionResult UpdateGet(int billId, int positionId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            var position = _billDbAccessor.GetBillById(billId)
                .Positions
                .FirstOrDefault(p => p.Id == positionId);

            return View("UpdatePartial", position.ToInteractionLevelPosition());
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Update")]
        public IActionResult Update(int billId, int positionId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.DeleteById(positionId);
            _positionsDbAccessor.AddPosition(position.ToPosition(billId));
            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
