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
            EditDescription(this[index], newDescription);
        }

        private void EditDescription(ToDoItem item, string newDescription)
        {
            item.Description = newDescription;
        }

        public void MarkAsComplete(int index)
        {
            MarkAsComplete(this[index]);
        }

        private void MarkAsComplete(ToDoItem item)
        {
            item.IsComplete = true;
        }
        
        public void Delete(int index)
        {
            Remove(this[index]);
        }
    }
}
