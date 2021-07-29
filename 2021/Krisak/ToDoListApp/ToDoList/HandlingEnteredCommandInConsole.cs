using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public class HandlingEnteredCommandInConsole
    {
        Stack<List<object>> _rollback = new Stack<List<object>>();

        public bool IsFinished { get; private set; } = false;

        public void HandleInput(List<Note> notes, string userInput)
        {
            var partsCommand = userInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (partsCommand.Length == 0)
                throw new WrongEnteredCommandException("Command entered empty.");

            SelectAndCallCommand(notes, partsCommand);
        }

        void SelectAndCallCommand(List<Note> notes, string[] partsCommand)
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

                case Commands.Rollback:
                    Rollback(notes, partsCommand);
                    return;

                case Commands.Show:
                    Show(notes);
                    return;

                case Commands.Exit:
                    IsFinished = true;
                    return;

                default:
                    throw new WrongEnteredCommandException("Wrong command entered.");
            }
        }

        void Add(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandAdd(partsCommand);

            _rollback.Push(CreatorRollbackCommands.CreateListCommandDelete());
            PerformerCommands.Add(notes, parsedCommand);
            return;
        }

        void Edit(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandEdit(partsCommand);

            _rollback.Push(CreatorRollbackCommands.CreateListCommandEdit(parsedCommand[1], notes[(int)parsedCommand[1]].Text));
            PerformerCommands.Edit(notes, parsedCommand);
            return;
        }

        void Toggle(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandToggle(partsCommand);

            _rollback.Push(CreatorRollbackCommands.CreateListCommandToggle(parsedCommand[1]));
            PerformerCommands.Toggle(notes, parsedCommand);
            return;
        }

        void Delete(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandDelete(partsCommand);

            _rollback.Push(CreatorRollbackCommands.CreateListCommandAdd(parsedCommand[1], notes[(int)parsedCommand[1]]));
            PerformerCommands.Delete(notes, parsedCommand);
            return;
        }

        void Rollback(List<Note> notes, string[] partsCommand)
        {
            var parsedCommand = CommandParser.ParseCommandRollback(partsCommand);

            PerformerCommands.Rollback(notes, _rollback, parsedCommand);
            return;
        }

        void Show(List<Note> notes)
        {
            PerformerCommands.Show(notes);
            return;
        }
    }
}
