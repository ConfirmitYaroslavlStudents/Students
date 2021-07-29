using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class CommandParser
    {
        public static List<object> ParseCommandAdd(string[] partsCommand)
        {
            var command = partsCommand[0];
            var note = ConvertArrayToString(1, partsCommand);

            return new List<object> { command, note };
        }

        public static List<object> ParseCommandEdit(string[] partsCommand)
        {
            var command = partsCommand[0];
            var index = ValidationNumberInCommands.IntTryParseInputIndex(partsCommand[1]);
            var note = ConvertArrayToString(2, partsCommand);

            return new List<object> { command, index, note };
        }

        public static List<object> ParseCommandToggle(string[] partsCommand)
        {
            var command = partsCommand[0];
            var index = ValidationNumberInCommands.IntTryParseInputIndex(partsCommand[1]);

            return new List<object> { command, index };
        }

        public static List<object> ParseCommandDelete(string[] partsCommand)
        {
            var command = partsCommand[0];
            var index = ValidationNumberInCommands.IntTryParseInputIndex(partsCommand[1]);

            return new List<object> { command, index };
        }

        public static List<object> ParseCommandRollback(string[] partsCommand)
        {
            var command = partsCommand[0];
            int count = ValidationNumberInCommands.IntTryParseInputCountForRollback(partsCommand[1]);

            return new List<object> { command, count };
        }

        static string ConvertArrayToString(int startingIndexOfNote, string[] command)
        {
            var newNote = new StringBuilder("");

            for (var i = startingIndexOfNote; i < command.Length; i++)
                newNote.Append(command[i] + " ");

            return newNote.ToString().Trim();
        }
    }
}
