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
            var toDoListMenu = args.Length == 0 ? new Menu(new HttpRequestGenerator(), new UserInputReader(), new ConsoleWriter()) : 
                                                  new Menu(new HttpRequestGenerator(), new CommandReader(args), new ConsoleWriter());
            await toDoListMenu.StartMenu();
        }
    }
}
