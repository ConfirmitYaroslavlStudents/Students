using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/SelectedTags")]
    [ApiController]
    public class SelectedTagsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public SelectedTagsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectedTag>>> GetSelectedTags()
        {
            return await _context.SelectedTags.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SelectedTag>> GetSelectedTag(long id)
        {
            var selectedTag = await _context.SelectedTags.FindAsync(id);

            if (selectedTag == null)
            {
                return NotFound();
            }

            return selectedTag;
        }
        [HttpPost]
        public async Task<ActionResult<SelectedTag>> PostSelectedTag(SelectedTag selectedTag)
        {
            _context.SelectedTags.Add(selectedTag);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSelectedTag), new { id = selectedTag.Id }, selectedTag);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutSelectedTag(long id, SelectedTag selectedTag)
        {
            if (id != selectedTag.Id)
            {
               return BadRequest();
            }

            if (!SelectedTagExists(id))
            {
                return NotFound();
            }

            _context.Entry(selectedTag).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSelectedTag(long id)
        {
            var selectedTag = await _context.SelectedTags.FindAsync(id);

            if (selectedTag == null)
            {
                return NotFound();
            }

            _context.SelectedTags.Remove(selectedTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool SelectedTagExists(long id) =>
     _context.SelectedTags.Any(e => e.Id == id);
    }
}
