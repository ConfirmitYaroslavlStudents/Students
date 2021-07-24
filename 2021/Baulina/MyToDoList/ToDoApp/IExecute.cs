using System;

namespace ToDoApp
{
    public interface IExecute
    {
        void ProcessOperation(string operationName);
        void RunCommand(Action command);
        void Add();
        void Edit();
        void Complete();
        void Delete();
        void List();
        void Error();
    }

}
