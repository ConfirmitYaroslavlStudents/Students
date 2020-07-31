using System;
using BillSplitter.Data;
using BillSplitter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillSplitter.Controllers
{
    public class BillController : Controller
    {
        private BillsDbAccessor _billDbAccessor;

        public BillController(BillContext context)
        {
            _billDbAccessor = new BillsDbAccessor(context);
        }

        [Authorize]
        [HttpGet]
        [Route("Bill/{billId}")]
        public IActionResult Index(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            var bill = _billDbAccessor.GetBillById(billId);

            return View(bill); //поскольку у нас там будет совокупность вьюх, то полагаю, надо передавать так.
        }

        [Authorize]
        [HttpGet]
        [Route("Bill/Create/{billName}")]
        public IActionResult Create(string billName)
        {
            var bill = new Bill(); //потом появится имя 

            _billDbAccessor.AddBill(bill);

            return RedirectToAction(nameof(Manage), new { billId = bill.Id });

        }

        [Authorize]
        [HttpGet]
        [Route("Bill/Manage/{billId}")]
        public IActionResult Manage(int billId)
        {
            return View(billId); 
        }

        [Authorize]
        [HttpPost]
        [Route("Bill/{billId}")]
        public IActionResult Delete(int billId)
        {
            if (!_billDbAccessor.DbContains(billId))
                throw new Exception();

            _billDbAccessor.DeleteById(billId);

            return RedirectToAction(nameof(Index), new { billId });
        }
    }
}
