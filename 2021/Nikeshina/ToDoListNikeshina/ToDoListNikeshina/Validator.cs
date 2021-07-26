using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Validator
    {
        public static bool IsStringValid(string dscr)
        {
            if (dscr.Length == 0)
                return false;

            return true;
        }

        public static bool IsDigitValid(string input, int count)
        {
            if (Int32.TryParse(input, out int num) && IsNumberCorrect(num, count))
                return true;

            return false; ;
        }
        public static bool IsNumberCorrect(int num, int count)
        {
            if (num > 0 && num <= count)
                return true;

            return false;
        }
    }
}
