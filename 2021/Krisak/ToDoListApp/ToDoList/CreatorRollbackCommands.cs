using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public static class CreatorRollbackCommands
    { 
        public static List<object> CreateListCommandAdd (object index, object note )
        {
            return new List<object>{ CommandsRollback.Add, index, note };
        }

        public static List<object> CreateListCommandEdit(object index, object noteText)
        {
            return new List<object> { CommandsRollback.Edit, index, noteText };
        }

        public static List<object> CreateListCommandToggle(object index)
        {
            return new List<object> { CommandsRollback.Toggle, index };
        }

        public static List<object> CreateListCommandDelete()
        {
            return new List<object> { CommandsRollback.Delete};
        }
    }
}
