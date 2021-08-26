using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/SelectedToDoItemsLogicalOperatorAnd")]
    [ApiController]
    public class SelectedToDoItemsLogicalOperatorAndController : ControllerBase
    {
        private readonly ToDoContext _context;

        public SelectedToDoItemsLogicalOperatorAndController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            var selectedTags = _context.SelectedTags.Select(e => e.TagId);

            var toDoItems = from ToDoItems in _context.ToDoItems
                        join TagToDoItems in _context.TagToDoItems on new { Id = ToDoItems.Id } equals new { Id = TagToDoItems.ToDoItemId }
                        join Tags in _context.Tags on new { Id = TagToDoItems.TagId } equals new { Id = Tags.Id }
                        where
                          selectedTags.Contains(Tags.Id)
                        group ToDoItems by new
                        {
                            ToDoItems.Id,
                            ToDoItems.Description,
                            ToDoItems.Status
                        } into g
                        where g.Count() == selectedTags.Count()
                        select new ToDoItem
                        {
                            Id = g.Key.Id,
                            Description = g.Key.Description,
                            Status = g.Key.Status
                        };

            return await toDoItems.ToListAsync();

        }
    }
}
