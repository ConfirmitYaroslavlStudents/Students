using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/SelectedTagsForToDoItem")]
    [ApiController]
    public class SelectedTagsForToDoItemController : ControllerBase
    {
        private readonly ToDoContext _context;

        public SelectedTagsForToDoItemController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags(long id)
        {
            var tags =  from tag in _context.Tags
                             join tagToDoItem in _context.TagToDoItems on tag.Id equals tagToDoItem.TagId
                             where tagToDoItem.ToDoItemId == id
                             select tag;

            return await tags.ToListAsync();
        }
    }
}
