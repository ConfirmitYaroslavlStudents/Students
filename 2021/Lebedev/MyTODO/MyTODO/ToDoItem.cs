using System;
using System.Collections.Generic;
using System.Text;

namespace MyTODO
{
    public class ToDoItem
    {
        public string Name
        {
            get;
            private set;
        }
        public int State
        {
            get;
            private set;
        }
        
        public ToDoItem(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
            State = 0;
        }

        public ToDoItem(string name, int done)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
            State = done;
        }

        public void Complete()
        {
            if (State != 0 )
                return;
            State = (int)States.completed;
        }
        
        public void Delete()
        {
            switch(State)
            {
                case (int)States.uncompleted:
                    State = (int)States.deleted;
                    break;
                case (int)States.completed:
                    State = (int)States.completeddeleted;
                    break;
                default:
                    return;
            }
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
            switch(State)
            {
                case (int)States.completed:
                    builder.Append(" Completed");
                    break;
                case (int)States.completeddeleted:
                    builder.Append(" Completed and Deleted");
                    break;
                case (int)States.deleted:
                    builder.Append(" Deleted");
                    break;
                case (int)States.uncompleted:
                    builder.Append(" Uncompleted");
                    break;
            }
            return builder.ToString();
        }
    }
}
