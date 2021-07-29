using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class HandlingEnteredCommandInCMD
    {
        public static void HandleInput(List<Note> notes, string[] userInput)
        { 
            if (userInput.Length == 0)
                throw new WrongEnteredCommandException("Command entered empty.");

            SelectAndCallCommand(notes, userInput);
        }

        static void SelectAndCallCommand(List<Note> notes, string[] partsCommand)
        {
            switch (partsCommand[0])
            {
                case Commands.Add:
                    Add(notes, partsCommand);
                    return;
                case Commands.Edit:
                    Edit(notes, partsCommand);
                    return;
                case Commands.Toggle:
                    Toggle(notes, partsCommand);
                    return;
                case Commands.Delete:
                    Delete(notes, partsCommand);
                    return;
                default:
                    throw new WrongEnteredCommandException("Wrong command entered.");
            }
        }

        static void Add(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandAdd(partsCommand);

            PerformerCommands.Add(notes, parsedCommand);
            return;
        }

        static void Edit(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandEdit(partsCommand);

            PerformerCommands.Edit(notes, parsedCommand);
            return;
        }

        static void Toggle(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandToggle(partsCommand);

            PerformerCommands.Toggle(notes, parsedCommand);
            return;
        }

        static void Delete(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandDelete(partsCommand);

            PerformerCommands.Delete(notes, parsedCommand);
            return;
        }
    }
}
