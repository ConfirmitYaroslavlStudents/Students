using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListProject
{
    public interface IWriterReader
    {
        public void Write(string message);

        public string Read();
    }
}
