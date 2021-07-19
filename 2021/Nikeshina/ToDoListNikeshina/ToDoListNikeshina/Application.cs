using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class Application
    {
        internal ToDoList _list;
        ILogger _logger;

        public Application() { }

        public Application(ILogger logger)
        {
            _logger = logger;
            _list = new ToDoList(_logger);
        }

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
                _logger.WriteLine(Messages.ToDoListIsEmpty());
                return;
            }

            _list.Print();
        }

        public void Add()
        {
           _logger.WriteLine(Messages.RequestDescription());

            var dscr = _logger.ReadLine();

            if(!IsDataValidString(dscr))
                return;

            _list.Add(new Task(dscr, false));
        }

       private bool IsDataValidString(string dscr)
        {
            if (dscr.Length == 0)
            {
                _logger.WriteLine(Messages.WrongFormatOfInputData());
                return false;
            }

            return true;
        }

        private bool IsDataValidDigit(string input)
        {
            if (Int32.TryParse(input, out int num) && IsNumberCorrect(num))
                return true;
             
            _logger.WriteLine(Messages.WrongFormatOfInputData());
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
            _logger.WriteLine(Messages.RequestNumber());

            var inputStr = _logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            _list.Delete(num);
        }

        public void Edit()
        {
            _logger.WriteLine(Messages.RequestNumber());

            var inputStr = _logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            _logger.WriteLine(Messages.RequestDescription());

            var dscr = _logger.ReadLine();

            if (!IsDataValidString(dscr))
                return;

            _list.Edit(num, dscr);
        }

        public void ChangeStatus()
        {
            _logger.WriteLine(Messages.RequestNumber());
            

            var inputStr = _logger.ReadLine();

            if (!IsDataValidDigit(inputStr))
                return;

            int num = int.Parse(inputStr);

            _list.ChangeStatus(num);
        }
    }
}
