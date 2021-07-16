using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class Application
    {
        private ToDoList _list = new ToDoList();

        public void Read()
        {
            _list.Read();
        }

        public void Write()
        {
            _list.Write();
        }

        public void Print()
        {
            if (_list.Count() == 0)
            {
                MessageWriter.ToDoListIsEmpty();
                return;
            }

            _list.Print();
        }

        public void Add()
        {
            MessageWriter.RequestDescription();

            var dscr = Console.ReadLine();

            if(!IsDataValidString(dscr))
                return;

            _list.Add(new Task(dscr, false));
        }

       public bool IsDataValidString(string dscr)
        {
            if (dscr.Length == 0)
            {
                MessageWriter.WrongFormatOfInputData();
                return false;
            }

            return true;
        }

        private bool IsDataValidDigit(string input)
        {
            if (Int32.TryParse(input, out int num) && IsNumberCorrect(num))
                return true;
             
            MessageWriter.WrongFormatOfInputData();
            return false; ;
        }
         private bool IsNumberCorrect(int num)
        {
            if (num > 0 && num <= _list.Count())
                return true;

            return false;
        }

        public void Delete()
        {
            MessageWriter.RequestNumber();

            var inputStr = Console.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            _list.Detete(num);
        }

        public void Edit()
        {
            MessageWriter.RequestNumber();

            var inputStr = Console.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            MessageWriter.RequestDescription();

            var dscr = Console.ReadLine();

            if (!IsDataValidString(dscr))
                return;

            _list.Edit(num, dscr);
        }

        public void ChangeStatus()
        {
            MessageWriter.RequestNumber();

            var inputStr = Console.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            _list.ChangeStatus(num);
        }

    }
}
