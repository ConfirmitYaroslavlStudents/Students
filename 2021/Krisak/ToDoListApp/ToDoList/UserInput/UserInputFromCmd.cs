namespace ToDoLibrary
{
    public class UserInputFromCmd: IUserInput
    {
        private readonly string[] _command;
        public UserInputFromCmd(string[] command)
        {
            _command = command;
        }
        public string[] GetCommand()
        {
            return _command;
        }
    }
}