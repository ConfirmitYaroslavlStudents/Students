using System.Collections.Generic;

namespace ToDoLibrary.Commands
{
    public class RollbackCommand : ICommand
    {
        public int Count;
        public Rollback @Rollback;
        public void PerformCommand()
        {
            Rollback.PerformRollbacks(Count);
        }
    }
}