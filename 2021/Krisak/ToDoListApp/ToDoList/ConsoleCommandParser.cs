namespace ToDoLibrary
{
    public class ConsoleCommandParser
    {
        private readonly string[] _userInput;

        public ConsoleCommandParser(string[] userInput)
            => _userInput = userInput;
    }
}