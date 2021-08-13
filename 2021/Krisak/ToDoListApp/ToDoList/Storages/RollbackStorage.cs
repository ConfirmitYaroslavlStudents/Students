using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class RollbackStorage
    {
        private Stack<ICommand> _rollbacks = new Stack<ICommand>();

        public Stack<ICommand> Get()
        {
            return new Stack<ICommand>(_rollbacks);
        }

        public void Set(Stack<ICommand> rollbacks)
        {
            _rollbacks = new Stack<ICommand>(rollbacks);
        }
    }
}