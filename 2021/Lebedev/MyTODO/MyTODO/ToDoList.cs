using System.Collections.Generic;

namespace MyTODO
{
    public class ToDoList:List<ToDoItem>
    {
        public ToDoList()
        {
        }

        public ToDoList(IEnumerable<ToDoItem> list)
        {
            if (list==null)
                return;
            this.AddRange(list);
        }

        public void Add(string item)
        {
            this.Add(new ToDoItem(item));
        }

        public ToDoList FindAll(bool completed, bool deleted)
        {
            return new ToDoList(this.FindAll(x => (deleted || !x.Deleted) && (completed || !x.Completed)));
        }
    }
}
