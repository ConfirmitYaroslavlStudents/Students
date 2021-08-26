using System;
using System.Threading.Tasks;
using InputOutputManagers;
using ToDoApp.CustomClient;

namespace ToDoApp
{
    public class App
    {
        private readonly Client _client;

        public App(Client client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task Run(string[] args)
        {
            var isCommandLineExecuted = args.Length != 0;
            IConsoleExtended console = isCommandLineExecuted
                ? new CommandLineInteractor(args)
                : new ConsoleInteractor();
            var commandExecutor = new CommandExecutor(console, _client);
            var appController = new AppController(commandExecutor, console);
            AppEngine appEngine =
                isCommandLineExecuted
                    ? new CommandLineProcessor(appController)
                    : new ConsoleMenuProcessor(appController);

            await appEngine.Run();
        }
    }
}
