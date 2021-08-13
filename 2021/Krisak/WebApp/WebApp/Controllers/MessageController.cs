using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ConsoleLogger _consoleLogger = new ConsoleLogger();
        private readonly SaverAndLoader _saverAndLoader = new SaverAndLoader();

        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            var length = _saverAndLoader.Load();

            _consoleLogger.LogGet(length);
            return length;
        }

        [HttpPost]
        public int Post([FromBody] string message)
        {

            _saverAndLoader.Save(_saverAndLoader.Load()+message.Length);
            _consoleLogger.LogPost(message);
            return message.Length;
        }
    }
}
