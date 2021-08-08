using System.Threading.Tasks;
using InputOutputManagers;
using Microsoft.Extensions.Options;
using ToDoApp.CustomClient;
using ToDoApp.Settings;

namespace ToDoApp
{
    public class App
    {
        private readonly IOptions<ClientSettingsConfiguration> _configuration;

        public App(IOptions<ClientSettingsConfiguration> configuration)
        {
            _configuration = configuration;
        }

        public async Task Run(string[] args)
        {
            var isCommandLineExecuted = args.Length != 0;
            IConsoleExtended console = isCommandLineExecuted
                ? new CommandLineInteractor(args)
                : new ConsoleInteractor();
            var commandExecutor = new CommandExecutor(console, new Client(_configuration));
            var appController = new AppController(commandExecutor, console);
            AppEngine appEngine =
                isCommandLineExecuted
                    ? new CommandLineProcessor(appController)
                    : new ConsoleMenuProcessor(appController);

            await appEngine.Run();
        }
    }
}
