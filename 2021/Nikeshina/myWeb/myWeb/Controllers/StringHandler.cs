using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace myWeb.Controllers
{
    [ApiController]
    [Route("strings")]
    public class StringHandler : ControllerBase
    {
        private int sum;
        private readonly ILogger<StringHandler> _logger;

        public StringHandler(ILogger<StringHandler> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            _logger.LogInformation(sum.ToString());
            return sum;
        }

        [HttpPost]
        public int Post([FromBody] string text)
        {
            sum += text.Length;
            _logger.LogInformation(sum.ToString());
            return text.Length;
        }
    }
}
