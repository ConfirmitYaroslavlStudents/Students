using ToDoLibrary.Commands;
using ToDoLibrary.CommandValidators;
using ToDoLibrary.CommandValidators.ValidatorForUserInput;
using ToDoLibrary.Const;
using ToDoLibrary.Storages;

namespace ToDoLibrary.CommandCreator
{
    public class StartRollbackCommandCreator: AbstractCommandCreator
    {
        private readonly RollbacksStorage _storage;
        private readonly string[] _userInput;

        public StartRollbackCommandCreator(RollbacksStorage storage, string[] userInput)
        {
            _storage = storage;
            _userInput = userInput;
        }

        public override ICommand GetCommand()
        {
            return _userInput[0] == UpdateCommands.Rollback ? ParseAndCreateCommand() : base.GetCommand();
        }

        private StartRollbackCommand ParseAndCreateCommand()
        {
            var validator = new IntTryParseValidator(false, _userInput[1]);
            ValidationRunner.Run(validator);

            var count = int.Parse(_userInput[1]);

            return new StartRollbackCommand {RollbacksStorage = _storage, Count = count};
        }
    }
}