using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BillSplitter.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BillSplitter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public void AddNewBill([FromBody] Position[] positions)
        {
            ViewData["positions"] = positions;
            //Нужно теперь сделать новый Bill с этими позициями и запихать его в базу данных 
        }

        //Home/NewBill
        [HttpGet]
        public async Task<IActionResult> NewBill()
        {
            return View();
        }

    }
}
