using System;
using System.Collections.Generic;
using System.Linq;

namespace MyToDoList
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
            var toDoItem = new ToDoItem {Description = description};
            if (Count > 0)
                toDoItem.Id = this[^1].Id + 1;
            Add(toDoItem);
        }

        public void EditDescription(int id, string newDescription)
        {
            if (string.IsNullOrEmpty(newDescription))
                throw new ArgumentNullException();
            FindTask(id).Description = newDescription;
        }

        public void Complete(int id)
        {
            FindTask(id).IsComplete = true;
        }

        public void Delete(int id)
        {
            Remove(FindTask(id));
        }

        private ToDoItem FindTask(long id)
        {
            return this.First(x => x.Id == id);
        }


    }
}
