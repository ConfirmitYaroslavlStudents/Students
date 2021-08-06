using System.Collections.Generic;
using ToDoLibrary;

namespace TestProjectForToDoLibrary
{
    public class FakeUserInput: IUserInput
    {
        public Stack<string[]> Commands = new Stack<string[]>();
        public string[] GetCommand()
        {
            return Commands.Pop();
        }
    }
}