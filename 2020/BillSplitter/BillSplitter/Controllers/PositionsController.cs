using BillSplitter.Data;
using BillSplitter.Models.InteractionLevel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BillSplitter.Controllers
{
    public class PositionsController : Controller
    {
        private BillsDbAccessor _billDbAccessor;
        private PositionsDbAccessor _positionsDbAccessor;
        private UsersDbAccessor _usersDbAccessor;
        private UserIdVisitor _userIdVisitor;

        public PositionsController(BillContext context, UserIdVisitor userIdVisitor)
        {
            _billDbAccessor = new BillsDbAccessor(context);
            _positionsDbAccessor = new PositionsDbAccessor(context);
            _usersDbAccessor = new UsersDbAccessor(context);
            _userIdVisitor = userIdVisitor;
        }

        [Authorize]
        [HttpGet]
        [Route("/Home/{userId}/Bill/Manage/{billId}/Positions")]
        public IActionResult Index(int userId, int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            var positions = _billDbAccessor.GetBillById(billId).Positions;

            return View(positions); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}/Positions")]
        public IActionResult Create(int billId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _positionsDbAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction("Manage","Bill", new { billId }); //...
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

        [Authorize]
        [HttpPost]
        [Route("Bill/Manage/{billId}/Positions/{positionId}")]
        public IActionResult Update(int billId, int positionId, InteractionLevelPosition position)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _positionsDbAccessor.DeleteById(positionId);
            _positionsDbAccessor.AddPosition(position.ToPosition(billId));

            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
