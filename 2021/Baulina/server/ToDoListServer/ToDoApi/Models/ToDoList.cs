using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ToDoApi.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public ToDoItemStatus Status { get; set; }
        public string Description { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class ToDoList : List<ToDoItem>
    {
        public ToDoList() { }

        public ToDoList(IEnumerable<ToDoItem> other)
        {
            foreach (var toDoItem in other)
            {
                AddToDoItem(toDoItem);
            }
        }

        public void AddToDoItem(ToDoItem toDoItem)
        {
            if (!Enum.IsDefined(typeof(ToDoItemStatus), toDoItem.Status)) throw new InvalidEnumArgumentException();
            if (Count > 0)
                toDoItem.Id = this[^1].Id + 1;
            Add(toDoItem);
        }

        public void Delete(int id)
        {
            Remove(FindToDoItem(id));
        }

        public ToDoItem FindToDoItem(int id)
        {
            return this.First(x => x.Id == id);
        }

        public IEnumerable<ToDoItem> GetItemsStartingWith(string prefix)
        {
            return FindAll(x => x.Description.StartsWith(prefix));
        }
    }
}