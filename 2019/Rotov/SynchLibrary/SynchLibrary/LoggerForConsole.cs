﻿using System;
using System.Collections.Generic;
namespace SynchLibrary
{
    public static class LoggerForConsole
    {
        public static void PrintLog(List<string> input)
        {
            foreach(var line in input)
                Console.WriteLine(line);
        }
    }
}
