namespace ToDoApp
{
    class ConsoleMenuProcessor : AppEngine
    {
        public sealed override ToDoAppMenu ToDoAppMenu { get; }

        public ConsoleMenuProcessor(ToDoAppMenu toDoAppMenu)
        {
            ToDoAppMenu = toDoAppMenu;
        }
        
        public override void Run()
        {
            while (ToDoAppMenu.CommandExecutor.IsWorking)
            {
                ToDoAppMenu.DoWork();
            }
        }
    }
}
