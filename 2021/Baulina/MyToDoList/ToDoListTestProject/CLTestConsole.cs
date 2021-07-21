using System.Collections.Generic;
using Spectre.Console.Rendering;
using ConsoleInteractors;

namespace ToDoListTestProject
{
    class ClTestConsole : IConsoleExtended
    {
        public List<string> Messages = new List<string>();
        private readonly List<string> _linesToRead = new List<string>();
        public ClTestConsole() { }
        public ClTestConsole(IEnumerable<string> linesToRead)
        {
            _linesToRead.AddRange(linesToRead);
        }

        public void WriteLine(string message) => Messages.Add(message);
        public string ReadLine()
        {
            if (_linesToRead.Count < 2) return string.Empty;
            var result = _linesToRead[1];
            _linesToRead.RemoveAt(1);
            return result;
        }
        public void RenderTable(IRenderable table) => Messages.Add("Rendered");
        public void Clear() => Messages.Add("Cleared");
        public string GetDescription() => ReadLine();
        public string GetMenuItemName() => _linesToRead[0].ToLower();
    }
}
