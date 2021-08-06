using ToDoLibrary.Loggers;
using System.IO;

namespace TestProjectForToDoLibrary
{
    public class FakeLogger: ILogger
    {
        public string Message;

        public FakeLogger()
        {
            File.Delete("MyNotes.txt");
        }
        public void Log(string message)
        {
            Message = message;
        }
    }
}