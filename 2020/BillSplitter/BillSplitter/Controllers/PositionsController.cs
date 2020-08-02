﻿using BillSplitter.Data;
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
        [HttpGet]
        [Route("Bills/{billId}/Positions/Create")]
        public IActionResult Create(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");
           
            ViewData["billId"] = billId;
            
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/Create")]
        public IActionResult Create(int billId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            ViewData["billId"] = billId;

            if (!ModelState.IsValid)
                return View(position);
        
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
        [Route("Bills/{billId}/Positions/{positionId}/Update")]
        public IActionResult Update(int billId, int positionId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");

            ViewData["billId"] = billId;

            var position = _billDbAccessor.GetBillById(billId)
                .Positions
                .FirstOrDefault(p => p.Id == positionId);

            return View(position.ToInteractionLevelPosition());
        }

        [Authorize]
        [HttpPost]
        [Route("Bills/{billId}/Positions/{positionId}/Update")]
        public IActionResult Update(int billId, int positionId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new NotImplementedException("Case is not implemented yet");
           
            ViewData["billId"] = billId;
           
            if (!ModelState.IsValid)
               return View(position);

            _positionsDbAccessor.UpdateById(positionId, position.ToPosition(billId));
          
            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
