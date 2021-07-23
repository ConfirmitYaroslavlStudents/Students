using System;
using System.Collections.Generic;

namespace MyToDoList
{
    public class ToDoItem
    {
        public bool IsComplete { get; set; }
        public string Description { get; set; }
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
            Add(new ToDoItem() {Description =  description});
        }

        public void EditDescription(int index, string newDescription)
        {
            if (string.IsNullOrEmpty(newDescription))
                throw new ArgumentNullException();
            this[index].Description = newDescription;
        }

        public void Complete(int index)
        {
            this[index].IsComplete = true;
        }

        public void Delete(int index)
        {
            Remove(this[index]);
        }
    }
}
