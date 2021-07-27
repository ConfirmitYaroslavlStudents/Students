using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MyToDoList;
using FileCommunicator;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoList _toDoList;
        private readonly FileManager _fileManager = new FileManager();
        private readonly ILogger<ToDoItemsController> _logger;

        public ToDoItemsController(ILogger<ToDoItemsController> logger)
        {
            _logger = logger;
            _toDoList = new ToDoList(_fileManager.LoadFromFile());
        }

        [HttpGet]
        public IEnumerable<ToDoItem> ViewAllTasks()
        {
            return _toDoList;
        }

        [HttpPost]
        public void Add([FromBody] string value)
        {
            ProcessTheRequest(() =>
            {
                var toDoItem = new ToDoItem {Description = value, IsComplete = false};
                _toDoList.Add(toDoItem);
            });
        }

        [HttpDelete("{index}")]
        public void Delete([FromBody] int index)
        {
            ProcessTheRequest(() => _toDoList.Delete(index));
        }

        [HttpPut("{index}/{newDescription}")]
        public void Edit([FromRoute] int index,[FromRoute] string newDescription)
        {
            ProcessTheRequest(() => _toDoList.EditDescription(index,newDescription));
        }

        [HttpPut("{index}")]
        public void Complete([FromBody] int index)
        {
            ProcessTheRequest(() => _toDoList.Complete(index));
        }

        public void ProcessTheRequest(Action request)
        {
            request();
            _fileManager.SaveToFile(_toDoList);
        }
    }
}
