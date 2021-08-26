using System;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace InputOutputManagers
{
    public class ConsoleInteractor : IConsoleExtended
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

        public int GetToDoItemStatus()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("[bold lightgoldenrod2_1] Choose the status (0 - not done, 1 - done) [/]")
                    .PageSize(12)
                    .AddChoices(0, 1));
        }

        public string GetMenuItemName()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold lightgoldenrod2_1] What do you want to do? [/]")
                    .PageSize(12)
                    .MoreChoicesText("[grey](Move up and down to reveal more operations)[/]")
                    .AddChoices("add", "edit", "complete", "incomplete", "delete", "list", "exit"));
        }
    }
}
