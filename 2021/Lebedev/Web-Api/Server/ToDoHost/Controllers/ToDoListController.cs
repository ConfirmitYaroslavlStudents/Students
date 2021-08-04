using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using MyTODO;
using System.Net;

namespace ToDoHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController : ControllerBase
    {
        public ToDoList Todo { get; set; }
        private readonly ILogger<ToDoListController> _logger;
        private ToDoFileManager _manager = new ToDoFileManager(new FileInfo($"TODOsave.txt"));

        public ToDoListController(ILogger<ToDoListController> logger)
        {
            Todo = new ToDoList(_manager.Read());
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ToDoItem> GetList()
        {
            _logger.Log(logLevel: LogLevel.Information, "Sent full todo list");
            return Todo;
        }

        [HttpGet("{id:int}")]
        public ToDoItem GetItem(int id)
        {
            if (id >= 0 && id < Todo.Count)
            {
                _logger.Log(logLevel: LogLevel.Information, $"Sent todo item {id}");
                return Todo[id];
            }

            _logger.Log(logLevel: LogLevel.Information, $"Todo item {id} not sent");
            throw new WebException("wrong id");
        }

        [HttpPatch]
        public string PatchItem([FromBody] ToDoItem item)
        {
            if (item.completed)
                Todo[item.id].Complete();
            if (item.deleted)
                Todo[item.id].Delete();
            if (!string.IsNullOrEmpty(item.name))
                Todo[item.id].ChangeName(item.name);
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Patch item {item.id} completed");
            return $"Patch item {item.id} completed";
        }

        [HttpPost]
        public string PostItem([FromBody] string name)
        {
            Todo.Add(name);
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Posted todo item {Todo.Count - 1} created");
            return "Post Completed";
        }

        [HttpDelete("{id:int}")]
        public string DeleteItem(int id)
        {
            Todo[id].Delete();
            _manager.Save(Todo);
            _logger.Log(logLevel: LogLevel.Information, $"Deleted todo item {id}");
            return "Delete Completed";
        }
    }
}