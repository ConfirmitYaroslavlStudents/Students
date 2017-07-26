using System;

namespace Mp3UtilLib.Logger
{
    public class ConsoleLogger
    {
        public void WriteError(string message)
        {
            Write(message, LogStatus.Error);
        }

        public void WriteSuccess(string message)
        {
            Write(message, LogStatus.Success);
        }

        public void Write(string message, LogStatus status)
        {
            WriteStatus(status);
            Console.Write($"{message}\n");
        }

        private void WriteStatus(LogStatus status)
        {
            switch (status)
            {
                case LogStatus.Success:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[Success] ");
                    break;
                case LogStatus.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[Error] ");
                    break;
                default:
                    return;
            }

            Console.ResetColor();
        }
    }
}