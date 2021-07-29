using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
   public static class ValidationNumberInCommands
    {
        public static int IntTryParseInputIndex(string inputIndex)
        {
            int result;
            if (!int.TryParse(inputIndex, out result))
                throw new WrongEnteredCommandException("Wrong note index.");

            return result - 1;
        }

        public static int IntTryParseInputCountForRollback(string inputCount)
        {
            int result;
            if (!int.TryParse(inputCount, out result))
                throw new WrongEnteredCommandException("Wrong note index.");

            return result;
        }

        public static bool IsIndexInRange(int index, int lowerLimit, int upperLimit)
        {
            if (index >= lowerLimit && index <= upperLimit)
                return true;
            return false;
        }
    }
}
