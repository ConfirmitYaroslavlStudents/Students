using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.SaveAndLoad;
using ToDoListLib;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        public ToDoList ToDoList { get; }
        private readonly IListSaveAndLoad _listSaveAndLoad;
        private readonly ILogger<ToDoItemsController> _logger;

        public ToDoItemsController(ILogger<ToDoItemsController> logger, IListSaveAndLoad listSaveAndLoad)
        {
            _logger = logger;
            _listSaveAndLoad = listSaveAndLoad;
            ToDoList = new ToDoList(_listSaveAndLoad.LoadTheList());
        }

        [HttpGet]
        public IEnumerable<ToDoItem> GetAllToDoItems()
        {
            return ToDoList;
        }

        [HttpPost]
        public Task<IActionResult> AddToDoItem([FromBody] string value)
        {
            return ProcessTheRequest(() =>
            {
                var toDoItem = new ToDoItem { Description = value, IsComplete = false };
                ToDoList.Add(toDoItem);
                _logger.LogInformation("The task has been added");
            });
        }

        [HttpDelete("{index}")]
        public Task<IActionResult> DeleteToDoItem(int index)
        {
            return ProcessTheRequest(() =>
            {
                ToDoList.Delete(index);
                _logger.LogInformation("The task has been deleted");
            });
        }

        [HttpPatch("{id}")]
        public Task<IActionResult> EditToDoItem(int id,[FromBody] JsonPatchDocument<ToDoItem> requestPatchDocument)
        {
            if (requestPatchDocument == null)
            {
                return Task.FromResult<IActionResult>(BadRequest());
            }
            
            return ProcessTheRequest(() =>
            {
                var existingToDoItem = ToDoList.FindTask(id);
                requestPatchDocument.ApplyTo(existingToDoItem);
                _logger.LogInformation("The task has been edited");
            });
        }

        private Task<IActionResult> ProcessTheRequest(Action request)
        {
            request();
            _listSaveAndLoad.SaveTheList(ToDoList);
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
