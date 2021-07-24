namespace ToDoApp
{
    class ConsoleMenuProcessor : AppEngine
    {
        public sealed override AppController ToDoAppController { get; }

        public ConsoleMenuProcessor(AppController toDoAppMenu)
        {
            ToDoAppController = toDoAppMenu;
        }
        
        public override void Run()
        {
            while (ToDoAppController.CommandExecutor.IsWorking)
            {
                ToDoAppController.SendOperationToExecutor();
            }
        }
    }
}
