using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public interface IApp
    {
        public void Save();
        public void Print();
        public void Add();
        public void Delete();
        public void Edit();
        public void ChangeStatus();

    }
}
