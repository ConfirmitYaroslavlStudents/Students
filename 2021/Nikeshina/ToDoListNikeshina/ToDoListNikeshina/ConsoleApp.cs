using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class ConsoleApp :App
    {
        public ConsoleApp()
        {
            Logger = new AppLogger();
            List = new ToDoList(new FileOperation(Logger).Load());
        }

        public ConsoleApp(ILogger logger)
        {
            Logger = logger;
            List = new ToDoList(new FileOperation(Logger).Load());
        }

        public override void Rollback()
        {
            Logger.Recording(Messages.RequestNumberOfCommand());

            var inputStr = Logger.TakeData();

            if (!Validator.IsNumberValid(inputStr,_lastLists.Count))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            List = GetLastList(num);
            Logger.Recording(Messages.IsDone());
        }

        private ToDoList GetLastList(int countOfStep)
        {
            if (countOfStep > _lastLists.Count)
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return List;
            }

            for (int i = 0; i < countOfStep - 1; i++)
                _lastLists.Pop();
            return _lastLists.Pop();
        }

        public override void StringHandling()
        {
            Logger.Recording(Messages.WriteInstuction());
            while(GetCommand(Logger.TakeData()));
        }
    }
}
