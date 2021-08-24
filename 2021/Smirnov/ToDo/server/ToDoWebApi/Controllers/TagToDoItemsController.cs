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
            var tagToDoItem = await _context.TagToDoItems.FindAsync(id);

            if (tagToDoItem == null)
            {
                return NotFound();
            }

            return tagToDoItem;
        }
        [HttpPost]
        public async Task<ActionResult<TagToDoItem>> PostTagToDoItem(TagToDoItem tagToDoItem)
        {
            _context.TagToDoItems.Add(tagToDoItem);

            await _context.SaveChangesAsync();

            return base.CreatedAtAction(nameof(GetTagToDoItem), new { id = tagToDoItem.Id }, tagToDoItem);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTagToDoItem(long id, TagToDoItem tagToDoItem)
        {
            if (id != tagToDoItem.Id)
            {
               return BadRequest();
            }

            if (!TagToDoItemsExists(id))
            {
                return NotFound();
            }

            _context.Entry(tagToDoItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTagToDoItem(long id)
        {
            var tagToDoItem = await _context.TagToDoItems.FindAsync(id);

            if (tagToDoItem == null)
            {
                return NotFound();
            }

            _context.TagToDoItems.Remove(tagToDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool TagToDoItemsExists(long id) =>
     _context.TagToDoItems.Any(e => e.Id == id);
    }
}
