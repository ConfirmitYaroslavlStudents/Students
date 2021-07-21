using System;

namespace ToDoApp
{
    class CommandLineProcessor : IMenuProcessor
    {
        MenuManager IMenuProcessor.MenuManager { get; set; }
        public ErrorPrinter ErrorPrinter { get; }

        public CommandLineProcessor()
        {
            ErrorPrinter = new ErrorPrinter();
        }

        public void Run()
        {
            try
            {
                ((IMenuProcessor) this).PrintMenu();
            }
            catch (IndexOutOfRangeException)
            {
                ErrorPrinter.PrintIncorrectNumberWarning();
            }
            catch (Exception)
            {
                ErrorPrinter.PrintErrorMessage();
            }
        }
    }
}
