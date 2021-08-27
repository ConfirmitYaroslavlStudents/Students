using ToDoLibrary.Commands;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.CommandValidators.ValidatorForUserInput;
using ToDoLibrary.Const;

namespace ToDoLibrary.CommandCreator
{
    public class EditTextCommandCreator: AbstractCommandCreator
    {
        private readonly string[] _userInput;

        public EditTextCommandCreator(string[] userInput)
            => _userInput = userInput;

        public override ICommand GetCommand()
        {
            return _userInput[0] == UpdateCommands.Edit ? ParseAndCreateCommand() : base.GetCommand();
        }

        private EditTextCommand ParseAndCreateCommand()
        {
            var validator = new IntTryParseValidator(false, _userInput[1]);
            ValidationRunner.Run(validator);

            var id = int.Parse(_userInput[1]);
            var text = string.Join(' ', _userInput, 2, _userInput.Length - 2);

            return new EditTextCommand {TaskId = id, Text = text};
        }
    }
}