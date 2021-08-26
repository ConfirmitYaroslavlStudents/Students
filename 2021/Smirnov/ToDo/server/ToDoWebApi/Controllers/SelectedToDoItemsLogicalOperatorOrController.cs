using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/SelectedToDoItemsLogicalOperatorOr")]
    [ApiController]
    public class SelectedToDoItemsLogicalOperatorOrController : ControllerBase
    {
        private readonly ToDoContext _context;

        public SelectedToDoItemsLogicalOperatorOrController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var selectedTags = await _context.SelectedTags.Select(e => e.TagId).ToListAsync();

            var toDoItems = (from toDoItem in _context.ToDoItems
                            join tagToDoItem in _context.TagToDoItems on toDoItem.Id equals tagToDoItem.ToDoItemId
                            join tag in _context.Tags on tagToDoItem.TagId equals tag.Id
                            where selectedTags.Contains(tag.Id)
                            select  toDoItem).Distinct();

            return await toDoItems.ToListAsync();
        }
    }
}