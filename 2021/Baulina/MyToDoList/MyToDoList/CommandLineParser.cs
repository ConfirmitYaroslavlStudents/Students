﻿using System.Collections.Generic;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace ToDoApp
{
    public class CommandLineParser : IConsole
    {
        private readonly List<string> _commandLineArguments = new();

        public CommandLineParser(IEnumerable<string> args)
        {
            _commandLineArguments.AddRange(args);
        }

        public void WriteLine(string message) { }

        public string ReadLine()
        {
            if (_commandLineArguments.Count < 2) return string.Empty;
            var result = _commandLineArguments[1];
            _commandLineArguments.RemoveAt(1);
            return result;
        }

        public void RenderTable(IRenderable table) => AnsiConsole.Render(table);

        public void Clear() { }

        public string GetDescription() => ReadLine();

        public string GetMenuItemName() => _commandLineArguments[0].ToLower();

    }
}
