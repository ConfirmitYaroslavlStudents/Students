using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        internal static string WrongFormatOfInputData()=> "Incorrect data";

        internal static  string ListIsEmpty()=> "List is empty(";

        internal static string RequestNumberOfString() => "Number of the note: ";

        internal static string RequestNumberOfCommand()=>"Number of commands: ";

        internal static string RequestDescription()=> "Description: " ;

        internal static string Completed()=> "Done! ";

        public static string InsructionText()
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
