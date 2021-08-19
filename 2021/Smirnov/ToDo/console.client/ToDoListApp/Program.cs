using System.Configuration;
using System.Threading.Tasks;
using ToDoListApp.Client;
using ToDoListApp.Reader;
using ToDoListApp.Writer;

namespace ToDoListApp
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;

            var toDoListMenu = args.Length == 0 ? new Menu(new HttpRequestGenerator(appSettings["AppPath"], appSettings["RequestPath"]),
                                                           new UserInputReader(new ConsoleInput()), new ConsoleWriter()) : 
                                                  new Menu(new HttpRequestGenerator(appSettings["AppPath"], appSettings["RequestPath"]), 
                                                           new CommandReader(args), new ConsoleWriter());

            await toDoListMenu.StartMenu();
        }
    }
}
