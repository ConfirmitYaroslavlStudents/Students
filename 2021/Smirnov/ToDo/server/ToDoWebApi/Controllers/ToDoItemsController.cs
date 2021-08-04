﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [Route("api/ToDoItems")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoItemsController(ToDoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItems.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchToDoItem(long id, PatchToDoItemDto toDoItem)
        {
            var oldToDoItem = await _context.ToDoItems.FindAsync(id);

            if (oldToDoItem == null)
            {
                return NotFound();
            }

            oldToDoItem.Description = toDoItem.IsFieldPresent(nameof(oldToDoItem.Description)) ? toDoItem.Description : oldToDoItem.Description;
            oldToDoItem.Status = toDoItem.IsFieldPresent(nameof(oldToDoItem.Status)) ? toDoItem.Status : oldToDoItem.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteToDoItem(long id)
        {
            var toDoItem = await _context.ToDoItems.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(long id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
