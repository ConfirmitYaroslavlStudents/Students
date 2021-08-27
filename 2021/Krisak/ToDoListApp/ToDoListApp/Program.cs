using ToDoLibrary;
using ToDoLibrary.CommandHandler;
using ToDoLibrary.Const;
using ToDoLibrary.Loggers;
using ToDoLibrary.SaveAndLoad;
using ToDoLibrary.Storages;
using TodoWeb;

namespace ToDoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger();
            var toDoApp = new ToDoApp(logger, new FileSaver(Data.SaveAndLoadFileName), new FileLoader(Data.SaveAndLoadFileName,logger));
            var userInputHandler = new UserInputHandler(toDoApp, new CommandMediator());

            if (args.Length == 0)
            {
                var rollback = new RollbacksStorage();
                var userInput = CommandReader.GetCommand();
                while (userInput[0] != QueriesCommand.Exit)
                {
                    userInputHandler.Run(userInput[0],new ConsoleCommandHandler(rollback,userInput));
                }
            }
            else
            {
                userInputHandler.Run(args[0], new CmdCommandHandler(args));
            }
        }
    }
}
