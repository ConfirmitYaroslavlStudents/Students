using System;
using ToDoLibrary.Commands;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.CommandValidators.ValidatorForUserInput;
using ToDoLibrary.Const;

namespace ToDoLibrary.CommandCreator
{
    public class ToggleCommandCreator: AbstractCommandCreator
    {
        private readonly string[] _userInput;

        public ToggleCommandCreator(string[] userInput)
            => _userInput = userInput;

        public override ICommand GetCommand()
        {
            return _userInput[0] == UpdateCommands.Toggle ? ParseAndCreateCommand() : base.GetCommand();
        }

        private ToggleStatusCommand ParseAndCreateCommand()
        {
            var validator = new IntTryParseValidator(false, _userInput[1]);
            var intStatusValidator = new IntTryParseValidator(true, _userInput[2]);
            var statusValidator = new TryParseStatusTaskValidator(false, _userInput[2]);
            validator.SetNext(intStatusValidator).SetNext(statusValidator);
            ValidationRunner.Run(validator);

            var id = int.Parse(_userInput[1]);
            var status = (StatusTask)Enum.Parse(typeof(StatusTask), _userInput[2]);

            return new ToggleStatusCommand {TaskId = id, Status = status};
        }
    }
}