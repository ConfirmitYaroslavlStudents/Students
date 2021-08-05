using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdApp
    {
        private readonly ILogger _logger;
        private readonly FileManager _fileManager;
        private readonly GeneralOperator _operator;
        private int _countTasksInProgress;
        public IGetInputData DataGetter { get;private set; }
        private ToDoList List { get; set; }
        

        public CmdApp(ILogger logger, IGetInputData dataGetter)
        {
            _logger = logger;
            DataGetter = dataGetter;
            _fileManager = new FileManager();
            List = new ToDoList(_fileManager.Load());
            _operator = new GeneralOperator(_logger, DataGetter, List);
            _countTasksInProgress = _fileManager.GetCountOfTaskInProgress();
        }

        public void AddNewTask()=> _operator.Add();

        public void ChangeStatus()
        {
            if (_operator.ChangeTaskStatus(_countTasksInProgress))
                _countTasksInProgress = _operator.GetCountOfTaskInProgress();
        }

        public void Delete()
        {
            if (_operator.Delete(_countTasksInProgress))
                _countTasksInProgress = _operator.GetCountOfTaskInProgress();
        }

        public void EditDescription()
        {
            var inputString = DataGetter.GetInputData();

            if (!Validator.IsNumberValid(inputString,List.Count()))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            int noteNumber = int.Parse(inputString);
            var newDescription = DataGetter.GetInputData();

            if (!Validator.IsStringValid(newDescription))
            {
                _logger.Log(Messages.WrongFormatOfInputData());
                return;
            }

            List.Edit(noteNumber, newDescription);
            _logger.Log(Messages.Completed());
        }

        public void Save() => _fileManager.Save(List._list);

        public void Print()=> _operator.Print();

        public List<Task> GetListOfTask() => List.GetListOfTask();

    }
}
