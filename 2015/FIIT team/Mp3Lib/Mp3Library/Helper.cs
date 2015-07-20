using System;
using System.Collections.Generic;

namespace Mp3Library
{
    public class Helper
    {
        private static readonly Dictionary<string, string> HelpMessages = new Dictionary<string, string>
        {
            {"help", ""},
            {"rename", ""},
            {"changeTag", ""}
        };

        public static void ShowInstructions(string[] args)
        {
            if (args.Length == 1)
            {
                foreach (var message in HelpMessages)
                {
                    Console.WriteLine(message.Key);
                    Console.WriteLine(message.Value);
                }
            }
            if (args.Length == 2)
            {
                Console.WriteLine(HelpMessages.ContainsKey(args[0])
                    ? HelpMessages[args[1]]
                    : "There is no such command!");
            }
        }
    }
}
