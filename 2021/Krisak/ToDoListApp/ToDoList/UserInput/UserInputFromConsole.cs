using System;

namespace ToDoLibrary
{
    public class UserInputFromConsole: IUserInput
    {
        public string[] GetCommand()
        {
            return Console.ReadLine().Split(" ");
        }
    }
}