using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdApp:IApp
    {
        private readonly ILogger _logger;
        private readonly GeneralOperator _operator;
        public IGetInputData dataGetter;
        private ToDoList List { get; set; }
        

        public CmdApp(ILogger logger, IGetInputData dataGetter, List<Task> listFromFile)
        {
            _logger = logger;
            this.dataGetter = dataGetter;
            List = new ToDoList(listFromFile);
            _operator = new GeneralOperator(_logger, this.dataGetter, List);
        }

        public void AddNewTask()=> _operator.Add();

        public void ChangeStatus()
        {
            _operator.ChangeTaskStatus();
        }

        public void Delete()
        {
            _operator.Delete();
        }

        public void EditDescription()
        {
            _operator.Edit();
        }

        public void Print()=> _operator.Print();

        public List<Task> GetListOfTask() => List.GetListOfTasks();

    }
}
