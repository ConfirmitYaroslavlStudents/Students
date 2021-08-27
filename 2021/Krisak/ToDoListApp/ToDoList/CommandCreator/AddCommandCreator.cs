using ToDoLibrary.Commands;
using ToDoLibrary.Const;

namespace ToDoLibrary.CommandCreator
{
    public class AddCommandCreator: AbstractCommandCreator
    {
        private readonly string[] _userInput;

        public AddCommandCreator(string[] userInput)
            => _userInput = userInput;

        public override ICommand GetCommand()
        {
            return _userInput[0] == UpdateCommands.Add ? ParseAndCreateCommand() : base.GetCommand();
        }

        private AddCommand ParseAndCreateCommand()
        {
            var text = string.Join(' ', _userInput, 1, _userInput.Length - 1);
            return new AddCommand {NewTask = new Task {Text = text}};
        }
    }
}