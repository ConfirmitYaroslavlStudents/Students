using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public static class Messages
    {
        public const string incorrectCommandsNumber = "Incorrect number!";
        public const string incorrectDescriptionLength = "Invalid description length!";
        public const string incorrectCountTasksInProgress = "Only three tasks are possible InProgress!";
        public const string incorrectTaskNumber = "Incorrect number of the task!";

        public const string listIsEmpty = "List is empty(";

        public const string requestNumberOfString = "Enter the number of the task";
        public const string requestNumberAndDescription = "First enter the number on a new line and then the new description";
        public const string requestNumberOfCommand="Enter the number of commands";
        public const string requestDescription = "Enter the description" ;

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
