using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class HandlingRollbackCommand
    {
        public static void Rollback(List<Note> notes, Stack<List<object>> rollback, int count)
        {
            while (count!=0)
            {
                var command = rollback.Pop();

                switch(command[0])
                {
                    case CommandsRollback.Add:
                       PerformerRollbackCommands.Add(notes, command);
                        break;
                    case CommandsRollback.Edit:
                       PerformerRollbackCommands.Edit(notes, command);
                        break;
                    case CommandsRollback.Toggle:
                        PerformerRollbackCommands.Toggle(notes, command);
                        break;
                    case CommandsRollback.Delete:
                        PerformerRollbackCommands.Delete(notes);
                        break;
                }

                count--;
            }
        }
    }
}
