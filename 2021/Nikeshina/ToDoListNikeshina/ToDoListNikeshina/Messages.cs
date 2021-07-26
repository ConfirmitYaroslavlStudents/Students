using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        public static string WrongFormatOfInputData()
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

        public static string WriteInstuction()
        {
            var sb = new StringBuilder();
            sb.AppendLine("----------------------------------------------------------");
            sb.Append("list - print ToDoList  ");
            sb.Append("add - add   ");
            sb.Append("delete - delete    ");
            sb.Append("edit - edit   ");
            sb.Append("change - change status   ");
            sb.AppendLine("exit - exit");
            sb.AppendLine("----------------------------------------------------------");
            return sb.ToString();
        }
    }
}
