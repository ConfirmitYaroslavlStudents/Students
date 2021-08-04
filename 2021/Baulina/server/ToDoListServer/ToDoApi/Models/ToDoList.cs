using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApi.Models
{
    public class ToDoItem : IEquatable<ToDoItem>
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public string Description { get; set; }
        public bool Equals(ToDoItem other)
        {
            return other != null && Id == other.Id && IsComplete == other.IsComplete && Description.Equals(other.Description);
        }
    }

    public class ToDoList : List<ToDoItem>
    {
        public ToDoList() { }

        public ToDoList(IEnumerable<ToDoItem> other)
        {
            AddRange(other);
        }

        public void Add(string description)
        {
            var toDoItem = new ToDoItem { Description = description };
            if (Count > 0)
                toDoItem.Id = this[^1].Id + 1;
            Add(toDoItem);
        }

        public void Delete(int id)
        {
            Remove(FindTask(id));
        }

        public ToDoItem FindTask(long id)
        {
            return this.First(x => x.Id == id);
        }
    }
}