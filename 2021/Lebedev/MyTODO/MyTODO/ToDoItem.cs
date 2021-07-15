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
            if (State == (int)States.deleted || State == (int)States.completeddeleted)
                return;
            if (State == 0)
                State = (int)States.deleted;
            else
                State = (int)States.completeddeleted;
        }
        
        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException();
            Name = name;
        }
    }
}
