using System.Collections.Generic;
using Spectre.Console.Rendering;
using ToDoApp;

namespace ToDoListTestProject
{
    class AppTestConsole : IConsole
    {
        public List<string> Messages = new List<string>();
        private readonly List<string> _linesToRead = new List<string>();

        public AppTestConsole() { }
        public AppTestConsole(IEnumerable<string> linesToRead)
        {
            _linesToRead.AddRange(linesToRead);
        }

        public void WriteLine(string message) => Messages.Add(message);
        public string ReadLine()
        {
            var result = _linesToRead[0];
            _linesToRead.RemoveAt(0);
            return result;
        }
        public void RenderTable(IRenderable table) => Messages.Add("Rendered");
        public void Clear() => Messages.Add("Cleared");
        public string GetDescription() => ReadLine();
        public string GetMenuItemName() => ReadLine();
    }
}
