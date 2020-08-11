using BillSplitter.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BillSplitter.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BillSplitter.Validators;
using System.Collections.Generic;
using BillSplitter.Models.ViewModels.PickPositions;

namespace BillSplitter.Controllers
{
    [Authorize]
    [Route("Bills/{billId}/Positions")]
    public class PositionsController : SuperController
    {
        public PositionsController(BillContext context) : base(context)
        {
           
        }

        private bool ValidatePosition(int billId, int positionId) // Move to attribute
        {
            var bill = Uow.Bills.GetBillById(billId);

            if (bill.Positions.Find(x => x.Id == positionId) == null)
                return false;

            return true;
        }

        [HttpGet]
        [Route("Pick")]
        public IActionResult PickPositions(int billId)
        {
            ViewData["billId"] = billId;

            var bill = Uow.Bills.GetBillById(billId);
            var customer = bill.Customers.FirstOrDefault(c => c.UserId == this.GetUserId());

            if (customer != null)
            {
                var positions = bill.Positions.OrderBy(p => p.Id).ToList();
                var model = new List<PositionViewModel>();
                foreach (var position in positions)
                {
                   
                    model.Add(GetPositionViewModel(position));
                }
                return View(model);
            }
            return RedirectToAction("JoinBill", "Bills", new { billId });
        }

        private PositionViewModel GetPositionViewModel(Position position)
        {
            var order = position.Orders.Find(x => x.Customer.UserId == GetUserId());
            return new PositionViewModel()
            {
                Name = position.Name,
                Id = position.Id,
                Selected = order != null,
                UserQuantity =  order!=null ? order.Quantity : null,
                PickedQuantity = position.Orders.Where(o => o.Quantity != null).Sum(o => o.Quantity * o.Position.Price).ToString() + "/" + position.Quantity,
                Orders = position.Orders,
                Price = position.Price
            };
        }


        [HttpGet]
        [Route("Manage")]
        [ValidateUser]
        public IActionResult ManagePositions(int billId)
        {
            ViewData["billId"] = billId;

            var positions = Uow.Bills.GetBillById(billId).Positions
                .OrderBy(p => p.Id)
                .ToList();

            return View(positions);
        }

        [HttpPost]
        [ValidateUser]
        public IActionResult Create(int billId, Position position)
        {
            Uow.Positions.Add(position);

            Uow.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId }); 
        }

        [HttpPost]
        [Route("{positionId}")]
        [ValidateUser]
        public IActionResult Delete(int billId, int positionId)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

            Uow.Positions.DeleteById(positionId);

            Uow.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId, });
        }

        [HttpPost]
        [Route("{positionId}/Update")]
        [ValidateUser]
        public IActionResult Update(int billId, int positionId, Position position)
        {
            if (!ValidatePosition(billId, positionId))
                return Error();

            Uow.Positions.UpdateById(positionId, position);

            Uow.Save();

            return RedirectToAction(nameof(ManagePositions), new { billId });
        }

        [HttpGet]
        [Route("{positionId}/Partial")] 
        public PartialViewResult GetPositionPartial(int billId, int positionId)
        {
            Position position =  Uow.Positions.GetById(positionId);
            return new PartialViewResult
            {
                ViewName = "PickPositionsPartial",
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = GetPositionViewModel(position)
                }
            };
        }
    }
}
