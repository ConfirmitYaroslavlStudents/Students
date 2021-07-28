using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyToDoList;
using FileCommunicator;

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

        private IEnumerable<ToDoViewItem> GetAllToDoItems()
        {
            return ConvertListOfToDoItemsToListOfToDoViewItems();
        }

        [HttpGet]
        public async Task<IEnumerable<ToDoViewItem>> GetAllToDoItemsAsync()
        {
            return await Task.FromResult(GetAllToDoItems());
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> AddToDoItem([FromBody] string value)
        {
            await ProcessTheRequest(() =>
            {
                var toDoItem = new ToDoItem { Description = value, IsComplete = false };
                ToDoList.Add(toDoItem);
            });
            return Ok();
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteToDoItem(int index)
        {
            return await ProcessTheRequest(() => ToDoList.Delete(index));
        }

        [HttpPut]
        public async Task<IActionResult> EditToDoItemDescription([FromBody] EditRequest editRequest)
        {
            return await ProcessTheRequest(() => ToDoList.EditDescription(editRequest.Index, editRequest.NewDescription));
        }

        [HttpPut("{index}")]
        public async Task<IActionResult> CompleteToDoItem(int index)
        {
            return await ProcessTheRequest(() => ToDoList.Complete(index));
        }

        private async Task<IActionResult> ProcessTheRequest(Action request)
        {
            try
            {
                request();
            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.LogInformation(e.Message, request.Method.Name);
                _logger.LogError(e, e.Message, request.Method.Name);
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message, request.Method.Name);
                _logger.LogError(e, e.Message, request.Method.Name);
                return BadRequest();
            }
            finally
            {
                _fileManager.SaveToFile(ToDoList);
            }
            return Ok();
        }

        private IEnumerable<ToDoViewItem> ConvertListOfToDoItemsToListOfToDoViewItems()
        {
            var list = ToDoList.Select((toDoItem, i) => new ToDoViewItem()
                {Description = toDoItem.Description, Index = i, IsComplete = toDoItem.IsComplete}).ToList();
            return list;
        }
    }
}
