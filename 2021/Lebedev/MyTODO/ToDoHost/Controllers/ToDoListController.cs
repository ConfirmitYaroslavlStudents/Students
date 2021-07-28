using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using MyTODO;
using Newtonsoft.Json;

namespace ToDoHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    { 
        ToDoListRestorer _restorer = new ToDoListRestorer(new FileInfo("TODOsave.txt"));

        public ToDoList Todo
        {
            get;
            set;
        }
        private readonly ILogger<ToDoListController> _logger;

        public ToDoListController(ILogger<ToDoListController> logger)
        {
            _logger = logger;
            Todo = new ToDoList(_restorer.Read());
        }

        [HttpGet]
        public IEnumerable<ToDoItem> GetList()
        {
            if (_logger != null)
                _logger.Log(logLevel: LogLevel.Information, "Sent full todo list");
            return Todo;
        }
        
        [HttpGet("GetItem")]
        public ToDoItem GetItem(int index)
        {
            try
            {
                if(index>0 && index<Todo.Count && _logger != null)
                    _logger.Log(logLevel: LogLevel.Information, $"Sent todo item {index}");
                return Todo[index];
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public string PostItem([FromBody]object item)
        {
            try
            {
                var state = JsonConvert.DeserializeObject<ToDoItem>(item.ToString());
                Todo.Add(state);
                _restorer.Save(Todo); 
                if(_logger != null)
                    _logger.Log(logLevel: LogLevel.Information, $"Deleted todo item {Todo.Count-1} created");
                return "Post Completed";
            }
            catch
            {
                return "Post Failed";
            }
        }

        [HttpDelete]
        public string DeleteItem(int index)
        {
            try
            {
                Todo[index].Delete();
                _restorer.Save(Todo);
                if (index > 0 && index < Todo.Count && _logger != null)
                    _logger.Log(logLevel: LogLevel.Information, $"Deleted todo item {index}");
                return "Delete Completed";
            }
            catch
            {
                return "Delete Failed";
            }
        }
    }
}