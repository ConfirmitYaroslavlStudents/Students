using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoListNikeshina
{
    public class CmdApp : App
    {
        public CmdApp(string[] args)
        {
            Logger = new CmdLogger(args);
            List = new ToDoList(new FileOperation(Logger).Load());
        }

        public override void Rollback()
        { }

        public override void StringHandling()
        {
            GetCommand(Logger.TakeData());
            Save();
        }
    }
}
