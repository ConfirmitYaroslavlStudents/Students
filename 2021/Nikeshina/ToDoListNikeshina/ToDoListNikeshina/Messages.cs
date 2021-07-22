using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        internal static string WrongFormatOfInputData()
        {
            return ("Incorrect data");
        }
        internal static  string ListIsEmpty()
        {
            return ("List is empty(");
        }
        internal static string RequestNumber()
        {
            return("Number of the note: ");
        }

        internal static string RequestDescription()
        {
            return("Description: ");
        }

        internal static string IsDone()
        {
            return ("Done! ");
        }
    }
}
