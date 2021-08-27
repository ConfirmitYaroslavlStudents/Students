using ToDoLibrary.Commands;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.CommandValidators.ValidatorForUserInput;
using ToDoLibrary.Const;

namespace ToDoLibrary.CommandCreator
{
    public class DeleteCommandCreator : AbstractCommandCreator
    {
        private readonly string[] _userInput;

        public DeleteCommandCreator(string[] userInput)
            => _userInput = userInput;

        public override ICommand GetCommand()
        {
            return _userInput[0] == UpdateCommands.Delete ? ParseAndCreateCommand() : base.GetCommand();
        }

        private DeleteCommand ParseAndCreateCommand()
        {
            var validator = new IntTryParseValidator(false, _userInput[1]);
            ValidationRunner.Run(validator);

            var id = int.Parse(_userInput[1]);

            return new DeleteCommand {TaskId = id};
        }
    }
}