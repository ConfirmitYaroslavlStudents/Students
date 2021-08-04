using System.Collections.Generic;
using InputOutputManagers;
using Spectre.Console.Rendering;

namespace ToDoListClientTests.Models
{
    class AppConsoleFake : IConsoleExtended
    {
        public List<string> Messages = new List<string>();
        public readonly List<string> LinesToRead = new List<string>();

        public AppConsoleFake() { }
        public AppConsoleFake(IEnumerable<string> linesToRead)
        {
            LinesToRead.AddRange(linesToRead);
        }

        public void WriteLine(string message) => Messages.Add(message);
        public virtual string ReadLine()
        {
            var result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }
        public void RenderTable(IRenderable table) => Messages.Add("Rendered");
        public void Clear() => Messages.Add("Cleared");
        public string GetDescription() => ReadLine();
        public virtual string GetMenuItemName() => ReadLine();
    }
}
