using System.Threading.Tasks;

namespace ToDoApp
{
    class ConsoleMenuProcessor : AppEngine
    {
        public sealed override AppController ToDoAppController { get; }

        public ConsoleMenuProcessor(AppController toDoAppMenu)
        {
            ToDoAppController = toDoAppMenu;
        }
        
        public override async Task Run()
        {
            while (ToDoAppController.CommandExecutor.IsWorking)
            {
                await ToDoAppController.SendOperationToExecutor();
            }
        }
    }
}
