﻿using System;

namespace Sync
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            PrintUsage();

            var arguments = new SyncConfigurator(args);
            
            if (arguments.ErrorMessage == String.Empty)
            {
                var synchronizator = arguments.GetSynchronizator();
                synchronizator.Run();
            }
            else
            {
                Console.WriteLine(arguments.ErrorMessage);
                PrintUsage();
            }

            Console.ReadKey();
        }

        private static void PrintUsage()
        {
            string message = @"Usage: Sync.exe <master> <slave> .. <slave>" + Environment.NewLine + 
                @"[-nodelete] [-logsilent] [-logsummary] [-logverbose]";

            Console.WriteLine(message);
        }
    }
}
