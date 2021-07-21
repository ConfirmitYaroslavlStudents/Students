using System;

namespace ToDoApp
{
    class ConsoleMenuProcessor:IMenuProcessor
    {
        MenuManager IMenuProcessor.MenuManager { get; set; }

        public ErrorPrinter ErrorPrinter { get; }

        public ConsoleMenuProcessor()
        {
            ErrorPrinter = new ErrorPrinter();
        }

        public void Run()
        {
            while (((IMenuProcessor) this).MenuManager.IsWorking)
            {
                try
                {

                    ((IMenuProcessor) this).PrintMenu();
                }
                catch (Exception)
                {
                    ErrorPrinter.PrintErrorMessage();
                }
            }
        }
    }
}
