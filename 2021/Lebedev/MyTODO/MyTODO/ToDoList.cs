using System.Collections.Generic;
using System.IO;

namespace MyTODO
{
    public class ToDoList:List<ToDoItem>
    {
        public ToDoList(FileInfo file)
        {
            var range = ToDoListReducer.Read(file);
            if(range!=null)
                this.AddRange(range);
        }

        public void Add(string item)
        {
            this.Add(new ToDoItem(item));
        }
    }
}
