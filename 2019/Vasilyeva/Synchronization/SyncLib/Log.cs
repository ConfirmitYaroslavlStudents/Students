using System;

namespace SyncLib
{
    internal class Log
    {
        private InfoLog infoLog;

        public Log(InfoLog infoLog, EnumLog logger)
        {
            this.infoLog = infoLog;
            Logger = logger;
        }

        public EnumLog Logger { get; }

        internal void Create()
        {
            switch(Logger)
            {
                case EnumLog.Summary:
                    CreateSummaryLog();
                    break;
                case EnumLog.Verbose:
                    CreateVerboseLog();
                    break;
            }
        }

        private void CreateVerboseLog()
        {
            switch(infoLog.Action)
            {
                case "copy":
                    Console.WriteLine($" {infoLog.TypeFile} {infoLog.FullName} was copied from {infoLog.Source}");
                    break;
                case "delete":
                    Console.WriteLine($" {infoLog.TypeFile} {infoLog.FullName} was deleted from {infoLog.Source}");
                    break;
                case "update":
                    Console.WriteLine($" {infoLog.TypeFile} {infoLog.FullName} was update by {infoLog.Source}");
                    break;
            }
        }

        private void CreateSummaryLog()
        {
            switch (infoLog.Action)
            {
                case "copy":
                    Console.WriteLine($"{infoLog.TypeFile} {infoLog.FullName} was copied");
                    break;
                case "delete":
                    Console.WriteLine($"{infoLog.TypeFile} {infoLog.FullName} was deleted");
                    break;
                case "update":
                    Console.WriteLine($"{infoLog.TypeFile} {infoLog.FullName} was update");
                    break;
            }
        }
    }
}