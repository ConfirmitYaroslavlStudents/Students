using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Validator
    {
        internal static bool IsStringValid(string dscr)
        {
            var length = dscr.Length;
            if (length == 0 || length>50)
                return false;

            return true;
        }

        internal static bool IsNumberValid(string input, int count)
        {
            if (Int32.TryParse(input, out int num) && IsNumberCorrect(num, count))
                return true;

            return false;
        }
        private static bool IsNumberCorrect(int num, int count)
        {
            if (num > 0 && num <= count)
                return true;

            return false;
        }

    }
}
