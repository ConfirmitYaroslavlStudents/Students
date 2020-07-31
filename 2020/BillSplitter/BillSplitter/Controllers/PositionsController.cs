using BillSplitter.Data;
using BillSplitter.Models;
using BillSplitter.Models.InteractionLevel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillSplitter.Controllers
{
    public class PositionsController : Controller
    {
        private BillsDbAccessor _billDbAccessor;
        private PositionsDbAccessor _positionsDbAccessor;

        public PositionsController(BillContext context)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("Bill/{billId}/Positions")]
        public IActionResult Index(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            var positions = _billDbAccessor.GetBillById(billId).Positions;

            return View(positions); //на самом деле тут надо передавать кое-что другое, ну ладно
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}/Positions")]
        public IActionResult Create(int billId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _positionsDbAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction("Manage","Bill", new { billId });
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}/Positions/{positionId}")]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _positionsDbAccessor.DeleteById(positionId);

            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
