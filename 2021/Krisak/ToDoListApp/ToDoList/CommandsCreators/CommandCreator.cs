using System.Collections.Generic;
using System.Text;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class CommandCreator
    {
        private readonly List<Task> _tasks;
        private const int TaskLengthLimit = 50;

        public CommandCreator(List<Task> tasks)
        {
            _tasks = tasks;
        }

        public ICommand CreateAddСommand(string[] partsCommand)
        {
            var text = ConvertArrayToString(1, partsCommand);

            if (text.Length > TaskLengthLimit)
                throw new  WrongEnteredCommandException($"Task length must not be more than {TaskLengthLimit} characters.");

            var task = new Task { Text = text };

            return new AddCommand { Tasks = _tasks, NewTask = task };
        }

        public ICommand CreateEditСommand(string[] partsCommand)
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(partsCommand[1]);
            var text = ConvertArrayToString(2, partsCommand);

            if (text.Length > TaskLengthLimit)
                throw new WrongEnteredCommandException($"Task length must not be more than {TaskLengthLimit} characters.");

            return new EditCommand {Tasks = _tasks, Index = index, Text = text};
        }

        public  ICommand CreateToggleСommand(string[] partsCommand)
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(partsCommand[1]);
            var numberStatus = ValidationNumberInCommands.IntTryParse(partsCommand[2]);

            if (numberStatus < 0 || numberStatus >2)
                throw new WrongEnteredCommandException("Wrong status number.");

            var newStatus = (StatusTask) numberStatus;

            return new ToggleCommand {Tasks = _tasks, Index = index, Status = newStatus};
        }

        public ICommand CreateDeleteСommand(string[] partsCommand)
        {
            var index = ValidationNumberInCommands.IntTryParseAndSubtract(partsCommand[1]);

            return new DeleteCommand {Tasks = _tasks, Index = index};
        }

        public ICommand CreateRollbackСommand(string[] partsCommand, Rollback rollback)
        {
            var count = ValidationNumberInCommands.IntTryParse(partsCommand[1]);

            return new RollbackCommand {Count = count, Rollback = rollback};
        }

        public ICommand CreateShowСommand()
        {
            return new ShowCommand{Tasks = _tasks};
        }

        private static string ConvertArrayToString(int startingIndexOfNote, string[] command)
        {
            var newNote = new StringBuilder("");

            for (var i = startingIndexOfNote; i < command.Length; i++)
                newNote.Append(command[i] + " ");

            return newNote.ToString().Trim();
        }
    }
}
