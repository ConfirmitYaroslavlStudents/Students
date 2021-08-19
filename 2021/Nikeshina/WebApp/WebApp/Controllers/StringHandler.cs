using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("myapp")]
    public class StringHandler : ControllerBase
    {
        private static int sum;

        private readonly ILogger<StringHandler> _logger;

        public StringHandler(ILogger<StringHandler> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<int> Get()
        {
            _logger.LogInformation(sum.ToString());
            return sum;
        }

        [HttpPost]
        public ActionResult<int> Post([FromBody] string text)
        {
            sum += text.Length;
            _logger.LogInformation(sum.ToString());
            return text.Length;
        }
    }
}
