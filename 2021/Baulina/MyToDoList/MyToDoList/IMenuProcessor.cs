namespace ToDoApp
{
    interface IMenuProcessor
    {
        public MenuManager MenuManager {get; protected internal set; }
        public ErrorPrinter ErrorPrinter { get; }
        void Run();
        public void HandleOperation(string operation)
        {
            switch (operation)
            {
                case "add":
                {
                    MenuManager.Add();
                    break;
                }
                case "edit":
                {
                    MenuManager.Edit();
                    break;
                }
                case "mark as complete":
                {
                    MenuManager.MarkAsComplete();
                    break;
                }
                case "delete":
                {
                    MenuManager.Delete();
                    break;
                }
                case "view all tasks":
                {
                    MenuManager.ViewAllTasks();
                    break;
                }
                case "exit":
                {
                    MenuManager.Exit();
                    break;
                }
                default:
                {
                    ErrorPrinter.PrintErrorMessage();
                    break;
                }
            }
        }

        public void PrintMenu()
        {
            var operation = MenuManager.GetMenuItemName();
            HandleOperation(operation);
        }
    }
}
