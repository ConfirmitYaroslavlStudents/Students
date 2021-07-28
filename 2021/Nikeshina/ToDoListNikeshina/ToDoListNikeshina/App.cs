using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public abstract class App 
    {
        public ToDoList List { get; set; }
        public ILogger Logger { get; set; }

        internal Stack<ToDoList> _lastLists = new Stack<ToDoList>();


        public void Add()
        {
            Logger.Recording(Messages.RequestDescription());

            var dscr = Logger.TakeData();

            if (!Validator.IsStringValid(dscr))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            PushListToStack();
            List.Add(new Task(dscr, false));
            Logger.Recording(Messages.IsDone());
        }

        public void ChangeStatus()
        {

            Logger.Recording(Messages.RequestNumberOfString());


            var inputStr = Logger.TakeData();

            if (!Validator.IsNumberValid(inputStr, List.Count()))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            PushListToStack();
            List.ChangeStatus(num);
            Logger.Recording(Messages.IsDone());
        }

        public void Delete()
        {
            Logger.Recording(Messages.RequestNumberOfString());

            var inputStr = Logger.TakeData();

            if (!Validator.IsNumberValid(inputStr, List.Count()))
            {
                Logger.Recording(Messages.WrongFormatOfInputData());
                return;
            }

            int num = int.Parse(inputStr);

            PushListToStack();
            List.Delete(num);
            Logger.Recording(Messages.IsDone());
        }

        public void Edit()
        {
            Logger.Recording(Messages.RequestNumberOfString());

            var inputStr = Logger.TakeData();

            if (!Validator.IsNumberValid(inputStr, List.Count()))
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

            PushListToStack();
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

        public abstract void Rollback();

        private void PushListToStack()
        {
            _lastLists.Push(new ToDoList(List.CopyList()));
        }

        public abstract void StringHandling();

        public bool GetCommand(string command)
        {
            switch (command)
            {
                case "list":
                    Print();
                    return true;
                case "add":
                    Add();
                    return true;
                case "delete":
                    Delete();
                    return true;
                case "change":
                    ChangeStatus();
                    return true;
                case "edit":
                    Edit();
                    return true;
                case "rollback":
                    Rollback();
                    return true;
                case "exit":
                    Save();
                    return false;
                default:
                    return true;
            }
        }
    }
}
