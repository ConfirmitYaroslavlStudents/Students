using Spectre.Console.Rendering;

namespace InputOutputManagers
{ 
    public interface IConsoleExtended
    {
        void WriteLine(string message);
        string ReadLine();
        void Clear();
        string GetDescription();
        string GetMenuItemName();
        void RenderTable(IRenderable table);
    }

    public static class ExtendedConsoleExtensions
    {
        public static void PrintErrorMessage(this IConsoleExtended console)
        {
            console.WriteLine("[red]Something went wrong...You might want to try one more time[/]");
        }

        public static void PrintIncorrectNumberWarning(this IConsoleExtended console)
        {
            console.WriteLine("[red]Incorrect id[/]");
        }

        public static void PrintNewDescriptionRequest(this IConsoleExtended console)
        {
            console.WriteLine("[lightgoldenrod2_1]Type in a new description[/]");
        }

        public static void PrintDoneMessage(this IConsoleExtended console)
        {
            console.Clear();
            console.WriteLine("[bold green]Done![/]");
        }

        public static void PrintTaskNumberRequest(this IConsoleExtended console)
        {
            console.WriteLine("[lightgoldenrod2_1]Choose the task number[/]");
        }
    }
}
