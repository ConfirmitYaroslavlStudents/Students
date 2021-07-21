namespace ToDoApp
{
    class ConsoleMenuProcessor : IMenuProcessor
    {
        private readonly MenuHandler _menuProcessor;

        public ConsoleMenuProcessor(MenuHandler menuProcessor)
        {
            _menuProcessor = menuProcessor;
        }

        public void Run()
        {
            while (_menuProcessor.CommandExecutor.IsWorking)
            {
                _menuProcessor.Handle();
            }
        }
    }
}
