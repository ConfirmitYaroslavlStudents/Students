using System;
using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary
{
    public class Rollback
    {
        private readonly Stack<ICommand> _rollback = new Stack<ICommand>();
        private readonly RollbackCommandCreator _rollbackCommandCreator;

        public Rollback(List<Task> tasks)
        {
            _rollbackCommandCreator = new RollbackCommandCreator(tasks);
        }

        public void PerformRollbacks(int count)
        {
            if (count < 1 || count > _rollback.Count)
                throw new WrongEnteredCommandException("Wrong count of rollback steps");

            for (var i = count; i > 0; i--)
            {
                var command = _rollback.Pop();

                command.PerformCommand();
                count--;
            }
        }
        
        public void AddNewRollback(string[] partsOfCommand)
        { 
            var command = _rollbackCommandCreator.AddNewCommand(partsOfCommand);
            if (command!=null)
                _rollback.Push(command);
        }
    }
}