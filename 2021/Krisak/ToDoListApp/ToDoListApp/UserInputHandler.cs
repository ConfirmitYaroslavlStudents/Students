using ToDoLibrary;
using ToDoLibrary.CommandHandler;
using TodoWeb;

namespace ToDoConsole
{
    public class UserInputHandler
    {
        private readonly ToDoApp _toDoApp;
        private readonly Mediator _mediator;
        public readonly QueriesPerformer @QueriesPerformer;

        public UserInputHandler(ToDoApp toDoApp, Mediator mediator)
        {
            _toDoApp = toDoApp;
            _mediator = mediator;
            @QueriesPerformer = new QueriesPerformer(_toDoApp);
        }

        public void Run(string command, ICommandHandler commandHandler)
        {
            _mediator.Send(this, command, commandHandler);
        }

        public void HandleCommand(ICommandHandler commandHandler)
        {
            _toDoApp.HandleCommand(commandHandler);
        }
    }
}