using System;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ConsoleInteractors
{
    public interface IConsole
    {
        void WriteLine(string message);
        string ReadLine();
    }

    public interface IConsoleExtended : IConsole
    {
        void RenderTable(IRenderable table);
        void Clear();
        string GetDescription();
        string GetMenuItemName();
    }

    public class MyConsole : IConsoleExtended
    {
        public void WriteLine(string message) => AnsiConsole.MarkupLine(message);
        public string ReadLine() => Console.ReadLine();
        public void RenderTable(IRenderable table) => AnsiConsole.Render(table);
        public void Clear() => Console.Clear();
        public string GetDescription()
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>("[bold lightgoldenrod2_1]What do you need to do? [/]")
                    .Validate(task =>
                    {
                        return task switch
                        {
                            null => ValidationResult.Error("[red]Incorrect task[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));
        }

        public string GetMenuItemName()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold lightgoldenrod2_1] What do you want to do? [/]")
                    .PageSize(12)
                    .MoreChoicesText("[grey](Move up and down to reveal more operations)[/]")
                    .AddChoices("add", "edit", "mark as complete", "delete", "view all tasks", "exit"));
        }
    }
}
