using System;
using mp3tager.Operations;

namespace mp3tager
{
    public abstract class Operation
    {
        public abstract void Call();

        public static Operation Create(string userInput)
        {
            switch (userInput)
            {
                case"changetags":
                    return new Changetags();
                case"rename":
                    return new Rename();
                case"analysis":
                    return new Analysis();
                case "sync":
                    return new Sync();
                case"exit":
                    return new Exit();
                default:
                    throw new ArgumentException("Invalid command");
            }
        }
    }
}
