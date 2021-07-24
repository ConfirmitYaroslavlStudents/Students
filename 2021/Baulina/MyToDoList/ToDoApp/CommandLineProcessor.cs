namespace ToDoApp
{
    class CommandLineProcessor : AppEngine
    {
        public sealed override AppController ToDoAppController { get; }
        public CommandLineProcessor(AppController toDoAppMenu)
        {
            ToDoAppController = toDoAppMenu;
        }

        public override void Run()
        {
            ToDoAppController.SendOperationToExecutor();
        }
    }
}
