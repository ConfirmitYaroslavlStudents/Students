using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/TagToDoItems")]
    [ApiController]
    public class TagToDoItemsController: ControllerBase
    {
        private readonly ToDoContext _context;

        public TagToDoItemsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagToDoItem>>> GetTagToDoItems()
        {
            return await _context.TagToDoItems.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TagToDoItem>> GetTagToDoItem(long id)
        {
            var toDoTag = await _context.TagToDoItems.FindAsync(id);

            if (toDoTag == null)
            {
                return NotFound();
            }

            return toDoTag;
        }
        [HttpPost]
        public async Task<ActionResult<TagToDoItem>> PostTagToDoItem(TagToDoItem TagToDoItem)
        {
            _context.TagToDoItems.Add(TagToDoItem);

            await _context.SaveChangesAsync();

            return base.CreatedAtAction(nameof(GetTagToDoItem), new { id = TagToDoItem.Id }, TagToDoItem);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTagToDoItem(long id, TagToDoItem TagToDoItem)
        {
            if (id != TagToDoItem.Id)
            {
               return BadRequest();
            }

            if (!TagToDoItemsExists(id))
            {
                return NotFound();
            }

            _context.Entry(TagToDoItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTagToDoItem(long id)
        {
            var tag = await _context.TagToDoItems.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            _context.TagToDoItems.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool TagToDoItemsExists(long id) =>
     _context.TagToDoItems.Any(e => e.Id == id);
    }
}
