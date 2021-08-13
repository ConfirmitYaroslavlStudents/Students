using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.HandlerСommand
{
    public class ShowCommandCreatorDecorator: AbstractCommandCreatorDecorator
    {
        private IShowTasks _showTasks;

        public ShowCommandCreatorDecorator(ICreator creator, IShowTasks showTasks) : base(creator) => _showTasks = showTasks;

        public override ICommand TryGetCommand(string[] partsOfCommand)
        {
            var command = base.Creator.TryGetCommand(partsOfCommand);
            if (command!=null)
                return command;

            return partsOfCommand[0] != AllCommands.Show ? null : new ShowCommand(_showTasks); ;
        }
    }
}