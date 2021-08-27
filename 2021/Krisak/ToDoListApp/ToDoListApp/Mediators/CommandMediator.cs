using ToDoConsole;
using ToDoLibrary.CommandHandler;

namespace TodoWeb
{
    public class CommandMediator: Mediator
    {
        public override void Send(UserInputHandler userInputHandler,string command, ICommandHandler commandHandler)
        {
            switch (command)
            {
                case QueriesCommand.Help:
                {
                    userInputHandler.QueriesPerformer.ShowHelp();
                    return;
                }
                case QueriesCommand.Show:
                {
                    userInputHandler.QueriesPerformer.ShowTasks();
                    return; 
                }
                default:
                {
                    userInputHandler.HandleCommand(commandHandler);
                    return;
                }
            }
        }
    }
}