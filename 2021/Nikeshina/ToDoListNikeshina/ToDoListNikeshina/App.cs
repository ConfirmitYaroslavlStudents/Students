using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public abstract class App : IApp
    {
        public ToDoList List { get; set; }
        public ILogger Logger { get; set; }

        public App() { }
        public App(ILogger logger)
        {
            Logger = logger;
            List = new ToDoList(logger);
        }

        public abstract void Add();

        public abstract void ChangeStatus();

        public abstract void Delete();

        public abstract void Edit();

        public bool IsDataValidString(string dscr)
        {
            if (dscr.Length == 0)
            {
                Logger.WriteLine(Messages.WrongFormatOfInputData());
                return false;
            }

            return true;
        }

        public bool IsDataValidDigit(string input)
        {
            if (Int32.TryParse(input, out int num) && IsNumberCorrect(num))
                return true;

            Logger.WriteLine(Messages.WrongFormatOfInputData());
            return false; ;
        }
        public bool IsNumberCorrect(int num)
        {
            if (num > 0 && num <= List.Count())
                return true;

            return false;
        }

        public void Print()
        {
            if (List.Count() == 0)
            {
                Logger.WriteLine(Messages.ListIsEmpty());
                return;
            }

            List.Print();
        }

        public void Read()
        {
            List.Read();
        }

        public void Write()
        {
            List.Write();
        }
    }
}
