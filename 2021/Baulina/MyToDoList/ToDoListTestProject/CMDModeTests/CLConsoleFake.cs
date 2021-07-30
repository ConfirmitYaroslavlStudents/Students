using System.Collections.Generic;
using ToDoListTestProject.ConsoleModeTests;

namespace ToDoListTestProject.CMDModeTests
{
    class ClConsoleFake : AppConsoleFake
    {
        public ClConsoleFake() { }
        public ClConsoleFake(IEnumerable<string> linesToRead)
        {
            LinesToRead.AddRange(linesToRead);
        }
        public override string ReadLine()
        {
            if (LinesToRead.Count < 2) return string.Empty;
            var result = LinesToRead[1];
            LinesToRead.RemoveAt(1);
            return result;
        }
        public override string GetMenuItemName() => LinesToRead[0].ToLower();
    }
}
