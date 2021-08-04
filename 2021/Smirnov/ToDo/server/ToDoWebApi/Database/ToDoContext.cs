using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Models;

namespace ToDoWebApi.Database
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
