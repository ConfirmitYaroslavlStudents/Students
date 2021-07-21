namespace ToDoApp
{
    interface IMenuProcessor
    {
        void Run();
    }

    class CommandLineProcessor : IMenuProcessor
    {
        private readonly MenuHandler _menuProcessor;

        public CommandLineProcessor(MenuHandler menuProcessor)
        {
            _menuProcessor = menuProcessor;
        }

        public void Run()
        {
            _menuProcessor.Handle();
        }
    }
}
