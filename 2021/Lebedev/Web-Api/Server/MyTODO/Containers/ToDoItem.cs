using System;
using System.Text;

namespace MyTODO
{
    public class ToDoItem
    {
        public int?  Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public bool? Completed
        {
            get;
            set;
        }

        public bool? Deleted
        {
            get;
            set;
        }

        public ToDoItem()
        {
        }

        public ToDoItem(int id)
        {
            this.Id = id;
            Name = "";
            Completed = false;
            Deleted = false;
        }

        public ToDoItem(int id, string name)
        {
            this.Id = id;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            this.Name = name;
            Completed = false;
            Deleted = false;
        }

        public ToDoItem(int id, string name, bool completed, bool deleted)
        {
            this.Id = id;
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            this.Name = name;
            this.Completed = completed;
            this.Deleted = deleted;
        }

        public void SetCompletedTrue()
        {
            if ((bool)Deleted)
                return;
            Completed = true;
        }
        
        public void SetDeletedTrue()
        {
            Deleted = true;
        }
        
        public void ChangeName(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException();
            Name = newName;
        }

        public override string ToString()
        {
            var builder = new StringBuilder(Name);
            builder.Append('\n');
            if ((bool)Completed)
                builder.Append("Completed\n");
            else
                builder.Append("\n");
            if ((bool)Deleted)
                builder.Append("Deleted");
            return builder.ToString();
        }
    }
}
