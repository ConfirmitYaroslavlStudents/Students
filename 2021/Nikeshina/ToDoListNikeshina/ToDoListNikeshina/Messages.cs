using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        public const string wrongFormatOfInputData = "Incorrect data";
        public const string listIsEmpty = "List is empty(";
        public const string requestNumberOfString = "Number of the note: ";
        public const string  requestNumberOfCommand="Number of commands: ";
        public const string requestDescription = "Description: " ;
        public const string completed = "Done! ";

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
