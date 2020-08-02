using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using BillSplitter.Models;

namespace BillSplitter.Controllers
{
    public class PositionsController : Controller // TODO ViewModels
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
        public IActionResult Create(int billId, Position position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.AddPosition(position);

            return RedirectToAction(nameof(Index), new { billId }); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}")]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.DeleteById(positionId);

            return RedirectToAction(nameof(Index), new { billId });
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Update")] // How to implement RESTful?
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            _positionsDbAccessor.UpdateById(positionId, position);
            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
