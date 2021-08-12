using ToDoListApp;
using ToDoListApp.Reader;

namespace ToDoListAppTests
{
    public class FakeUserInputReader : IReader
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public ListCommandMenu Command { set; get; }

        public string GetDescription()
        {
            return Description;
        }

        public int GetTaskId()
        {
            return Id;
        }

        public ListCommandMenu GetCommand()
        {
            return Command;
        }

        public bool ContinueWork()
        {
            return false;
        }
    }
}
