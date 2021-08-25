using System;
using System.Text;

namespace MyTODO
{
    public class ToDoItem
    {
        public int id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public bool completed
        {
            get;
            set;
        }

        public bool deleted
        {
            get;
            set;
        }

        public ToDoItem()
        {
        }

        public ToDoItem(int id)
        {
            this.id = id;
            name = "";
        }

        public ToDoItem(int id, string name)
        {
            this.id = id;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            this.name = name;
            completed = false;
            deleted = false;
        }

        public ToDoItem(int id, string name, bool completed, bool deleted)
        {
            this.id = id;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            this.name = name;
            this.completed = completed;
            this.deleted = deleted;
        }

        public void Complete()
        {
            if (deleted)
                return;
            completed = true;
        }
        
        public void Delete()
        {
            deleted = true;
        }
        
        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException();
            name = newName;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(name);
            builder.Append('\n');
            if (completed)
                builder.Append("Completed\n");
            else
                builder.Append("\n");
            if (deleted)
                builder.Append("Deleted");
            return builder.ToString();
        }
    }
}
