using ToDoConsole;
using ToDoLibrary.CommandHandler;

namespace TodoWeb
{
    public abstract class Mediator
    {
        public abstract void Send(UserInputHandler userInputHandler, string command, ICommandHandler commandHandler);
    }
}