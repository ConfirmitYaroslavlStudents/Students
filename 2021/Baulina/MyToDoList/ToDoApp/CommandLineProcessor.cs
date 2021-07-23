namespace ToDoApp
{
    class CommandLineProcessor : AppEngine
    {
        public sealed override ToDoAppMenu ToDoAppMenu { get; }
        public CommandLineProcessor(ToDoAppMenu toDoAppMenu)
        {
            ToDoAppMenu = toDoAppMenu;
        }

        public override void Run()
        {
            ToDoAppMenu.DoWork();
        }
    }
}
