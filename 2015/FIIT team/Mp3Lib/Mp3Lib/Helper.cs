using System;
using System.Collections.Generic;

namespace Mp3Lib
{
    //здесь планируется общий хэлп и для каждой команды
    class Helper
    {
        private readonly Dictionary<string, string> _helpMessages = new Dictionary<string, string>
        {
            {"help", ""},
            {"rename", ""},
            {"changeTag", ""}
        };

        public void ShowInstructions(string[] args)
        {
            if (args.Length == 1)
            {
                foreach (var message in _helpMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
            }
            if (args.Length == 2)
            {
                Console.WriteLine(_helpMessages.ContainsKey(args[0])
                    ? _helpMessages[args[1]]
                    : "There is no such command!");
            }
        }
    }
}
