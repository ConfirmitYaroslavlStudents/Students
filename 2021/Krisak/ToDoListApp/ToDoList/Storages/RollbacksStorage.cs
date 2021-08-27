using System.Collections.Generic;
using ToDoLibrary.CommandCreator;
using ToDoLibrary.Commands;

namespace ToDoLibrary.Storages
{
    public class RollbacksStorage
    {
        private readonly Stack<ICommand> _rollbacks = new Stack<ICommand>();

        public int Count => _rollbacks.Count;

        public ICommand Pop() 
            => _rollbacks.Pop();

        public void TryPush(ICommand command, List<Task> tasks)
        {
            var rollbackCommandCreator = new RollbackCommandsCreator(tasks);
            var rollbackCommand = rollbackCommandCreator.GetCommand(command);

            if (rollbackCommand != null)
                _rollbacks.Push(rollbackCommand);
        }
    }
}