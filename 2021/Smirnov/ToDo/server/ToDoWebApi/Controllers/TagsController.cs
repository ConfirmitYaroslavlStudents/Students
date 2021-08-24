using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/Tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public TagsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(long id)
        {
            var toDoTag = await _context.Tags.FindAsync(id);

            if (toDoTag == null)
            {
                return NotFound();
            }

            return toDoTag;
        }
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _context.Tags.Add(tag);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTag(long id, Tag tag)
        {
            if(id != tag.Id)
            {
               return BadRequest();
            }

            if (!TagExists(id))
            {
                return NotFound();
            }

            _context.Entry(tag).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTag(long id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool TagExists(long id) =>
     _context.Tags.Any(e => e.Id == id);
    }
}
