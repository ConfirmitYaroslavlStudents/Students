using System;
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
            State = (int)ToStates.Completed;
        }
        
        public void Delete()
        {
            switch(State)
            {
                case (int)ToStates.Uncompleted:
                    State = (int)ToStates.Deleted;
                    break;
                case (int)ToStates.Completed:
                    State = (int)ToStates.CompletedDeleted;
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
                case (int)ToStates.Completed:
                    builder.Append(" Completed");
                    break;
                case (int)ToStates.CompletedDeleted:
                    builder.Append(" Completed and Deleted");
                    break;
                case (int)ToStates.Deleted:
                    builder.Append(" Deleted");
                    break;
                case (int)ToStates.Uncompleted:
                    builder.Append(" Uncompleted");
                    break;
            }
            return builder.ToString();
        }
    }
}
