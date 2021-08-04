using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InputOutputManagers;

namespace ToDoApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isCommandLineExecuted = args.Length != 0;
            IConsoleExtended console = isCommandLineExecuted
                ? new CommandLineInteractor(args)
                : new ConsoleInteractor();
            var commandExecutor = new CommandExecutor(console, ConfigureWebClient());
            var appController = new AppController(commandExecutor, console);
            AppEngine appEngine =
                isCommandLineExecuted
                    ? new CommandLineProcessor(appController)
                    : new ConsoleMenuProcessor(appController);

            await appEngine.Run();
        }

        public static HttpClient ConfigureWebClient()
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:5000/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
