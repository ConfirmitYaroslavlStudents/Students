using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class App : IApp
    {
        public ToDoList List { get; set; }
        public ILogger Logger { get; set; }

        public App(ILogger logger)
        {
            Logger = logger;
            List = new ToDoList(new FileOperation(Logger).Load());
        }

        public void Add()
        {
            Logger.Recording(Messages.RequestDescription());

            var dscr = Logger.TakeData();

            if (!Validator.IsStringValid(dscr))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            List.Add(new Task(dscr, false));
            Logger.Recording(Messages.IsDone());
        }

        public void ChangeStatus()
        {

            Logger.Recording(Messages.RequestNumber());


            var inputStr = Logger.TakeData();

            if (!Validator.IsDigitValid(inputStr, List.Count()))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            List.ChangeStatus(num);
            Logger.Recording(Messages.IsDone());
        }

        public void Delete()
        {
            Logger.Recording(Messages.RequestNumber());

            var inputStr = Logger.TakeData();

            if (!Validator.IsDigitValid(inputStr, List.Count()))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            List.Delete(num);
            Logger.Recording(Messages.IsDone());
        }

        public void Edit()
        {
            Logger.Recording(Messages.RequestNumber());

            var inputStr = Logger.TakeData();

            if (!Validator.IsDigitValid(inputStr, List.Count()))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            Logger.Recording(Messages.RequestDescription());

            var dscr = Logger.TakeData();

            if (!Validator.IsStringValid(dscr))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            List.Edit(num, dscr);
            Logger.Recording(Messages.IsDone());
        }


        public void Print()
        {
            if (List.Count() == 0)
            {
                Logger.Recording(Messages.ListIsEmpty());
                return;
            }

             int i = 1;
            foreach (var task in List._list)
            {
                Logger.Recording(i + ". " + task.ToString());
                i++;
            }
        }

        public void Save()
        {
            var fileOperation = new FileOperation(Logger);
            fileOperation.Save(List._list);
        }
    }
}
