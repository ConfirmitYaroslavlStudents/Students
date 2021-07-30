using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyToDoList;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoItemsController : ControllerBase
    {
        public ToDoList ToDoList { get; }
        private readonly FileManager _fileManager = new FileManager();
        private readonly ILogger<ToDoItemsController> _logger;

        public ToDoItemsController(ILogger<ToDoItemsController> logger, IToDoListProvider toDoListProvider)
        {
            _logger = logger;
            ToDoList = new ToDoList(toDoListProvider.GetToDoList());
        }

        [HttpGet]
        public Task<IEnumerable<ToDoViewItem>> GetAllToDoItemsAsync()
        {
            return Task.FromResult(GetAllToDoItems());
        }

        private IEnumerable<ToDoViewItem> GetAllToDoItems()
        {
            return ConvertListOfToDoItemsToListOfToDoViewItems();
        }

        private IEnumerable<ToDoViewItem> ConvertListOfToDoItemsToListOfToDoViewItems()
        {
            var list = ToDoList.Select((toDoItem, i) => new ToDoViewItem()
                { Description = toDoItem.Description, Index = i, IsComplete = toDoItem.IsComplete }).ToList();
            return list;
        }

        [HttpPost]
        public Task<IActionResult> AddToDoItem([FromBody] string value)
        {
            return ProcessTheRequest(() =>
            {
                var toDoItem = new ToDoItem { Description = value, IsComplete = false };
                ToDoList.Add(toDoItem);
            });
        }

        [HttpDelete("{index}")]
        public Task<IActionResult> DeleteToDoItem(int index)
        {
            return ProcessTheRequest(() => ToDoList.Delete(index));
        }

        [HttpPut]
        public Task<IActionResult> EditToDoItemDescription([FromBody] EditRequest editRequest)
        {
            return ProcessTheRequest(() => ToDoList.EditDescription(editRequest.Index, editRequest.NewDescription));
        }

        [HttpPut("{index}")]
        public Task<IActionResult> CompleteToDoItem(int index)
        {
            return ProcessTheRequest(() => ToDoList.Complete(index));
        }

        private Task<IActionResult> ProcessTheRequest(Action request)
        {
            try
            {
                request();
                _fileManager.SaveToFile(ToDoList);
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogInformation(e.Message, request.Method.Name);
                _logger.LogError(e, e.Message, request.Method.Name);
                return Task.FromResult<IActionResult>(NotFound());
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, request.Method.Name);
                _logger.LogError(e, e.Message, request.Method.Name);
                return Task.FromResult<IActionResult>(BadRequest());
            }
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
