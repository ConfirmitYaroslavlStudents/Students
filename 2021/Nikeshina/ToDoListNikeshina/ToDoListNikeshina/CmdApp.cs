using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdApp
    {
        private readonly ILogger _logger;
        public IGetterInputData DataGetter { get;private set; }
        private ToDoList List { get; set; }
        private readonly FileManager _fileManager;
        private readonly CommonOperationWithToDo _printer;
        private  int _countTasksInProgress;

        public CmdApp(ILogger logger, IGetterInputData dataGetter)
        {
            _logger = logger;
            DataGetter = dataGetter;
            _fileManager = new FileManager();
            List = new ToDoList(_fileManager.Load());
            _printer = new CommonOperationWithToDo(_logger, DataGetter, List);
            _countTasksInProgress = _fileManager.GetCountOfTaskInProgress();
        }

        public void AddNewTask()
        {
            _printer.Add();
        }

        public void ChangeStatus()
        {
            if (_printer.ChangeTaskStatus(_countTasksInProgress))
                _countTasksInProgress = _printer.GetCountOfTaskInProgress();
        }

        public void Delete()
        {
            if (_printer.Delete(_countTasksInProgress))
                _countTasksInProgress = _printer.GetCountOfTaskInProgress();
        }

        public void EditDescription()
        {
            var inputStr = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputStr,List.Count()))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);
            var dscr = DataGetter.GetInputData();

            if (!Validator.IsStringValid(dscr))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            List.Edit(num, dscr);
            _logger.Log(Messages.Completed());
        }

        public void Save() => _fileManager.Save(List._list);

        public void Print()
        {
            _printer.Print();
        }

        public List<Task> GetListOfTask() => List.GetListOfTask();

    }
}
