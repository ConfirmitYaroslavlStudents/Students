using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class BaseCommandCreator: ICreator
    {
        private List<Task> _tasks;
        public BaseCommandCreator(List<Task> tasks) => _tasks = tasks;

        public  ICommand TryGetCommand(string[] partsOfCommand)
        {
            return partsOfCommand[0] switch
            {
                AllCommands.Add => Add(partsOfCommand),
                AllCommands.Edit => Edit(partsOfCommand),
                AllCommands.Toggle => Toggle(partsOfCommand),
                AllCommands.Delete => Delete(partsOfCommand),
                _ => null
            };
        }

        public ICommand Add(string[] partsOfCommand)
        {
            var addCommand = new AddCommand();
            addCommand.SetParameters(partsOfCommand);
            addCommand.RunValidate();
            return addCommand;
        }

        public ICommand Edit(string[] partsOfCommand)
        {
            var editCommand = new EditCommand();
            editCommand.SetParameters(partsOfCommand);
            editCommand.RunValidate(_tasks);
            return editCommand;
        }

        public ICommand Toggle(string[] partsOfCommand)
        {
            var toggleCommand = new ToggleCommand();
            toggleCommand.SetParameters(partsOfCommand);
            toggleCommand.RunValidate(_tasks);
            return toggleCommand;
        }

        public ICommand Delete(string[] partsOfCommand)
        {
            var deleteCommmand = new DeleteCommand();
            deleteCommmand.SetParameters(partsOfCommand);
            deleteCommmand.RunValidate(_tasks);
            return deleteCommmand;
        }
    }
}