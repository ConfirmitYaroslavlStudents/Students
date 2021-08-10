using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApi.SaveAndLoad;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("todo-list")]
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
        public ActionResult<IEnumerable<ToDoItem>> GetAllToDoItems()
        {
            return ToDoList;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ToDoItem> GetTodoItem(int id)
        {
            return ToDoList.FindToDoItem(id);
        }

        [HttpGet("{prefix}")]
        public IEnumerable<ToDoItem> GetTodoItemsStartingWith(string prefix)
        {
            return ToDoList.GetItemsStartingWith(prefix);
        }

        [HttpPost]
        public IActionResult AddToDoItem([FromBody] ToDoItem toDoItem)
        {
            ToDoList.AddToDoItem(toDoItem);
            _logger.LogInformation("The task has been added");
            _listSaveAndLoad.SaveTheList(ToDoList);
            return CreatedAtAction(nameof(GetTodoItem), new { id = toDoItem.Id }, toDoItem);
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteToDoItem(int index)
        {
            ToDoList.Delete(index);
            _logger.LogInformation("The task has been deleted");
            _listSaveAndLoad.SaveTheList(ToDoList);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult EditToDoItem(int id, [FromBody] JsonPatchDocument<ToDoItem> requestPatchDocument)
        {
            var existingToDoItem = ToDoList.FindToDoItem(id);
            requestPatchDocument.ApplyToSafely(existingToDoItem, ToDoList);

            _logger.LogInformation("The task has been edited");
            _listSaveAndLoad.SaveTheList(ToDoList);
            return NoContent();
        }

    }
}
