using System;
using System.Text;

namespace MyTODO
{
    public class ToDoItem
    {
        public string Name
        {
            get;
            set;
        }

        public bool Completed
        {
            get;
            set;
        }

        public bool Deleted
        {
            get;
            set;
        }

        public ToDoItem()
        {
            Name = "";
        }

        public ToDoItem(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
            Completed = false;
            Deleted = false;
        }

        public ToDoItem(string name, bool completed, bool deleted)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
            Completed = completed;
            Deleted = deleted;
        }

        public void Complete()
        {
            if (Deleted)
                return;
            Completed = true;
        }
        
        public void Delete()
        {
            Deleted = true;
        }
        
        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(Name);
            builder.Append('\n');
            if (Completed)
                builder.Append("Completed\n");
            else
                builder.Append("\n");
            if (Deleted)
                builder.Append("Deleted");
            return builder.ToString();
        }
    }
}
