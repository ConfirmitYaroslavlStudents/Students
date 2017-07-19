using System;

namespace Mp3UtilLib.Logger
{
    public class ConsoleLogger
    {
        public void Write(string message, LogStatus status)
        {
            WriteStatus(status);
            Console.Write($"{message}\n");
        }

        private void WriteStatus(LogStatus status)
        {
            switch (status)
            {
                case LogStatus.Info:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("[Info] ");
                    break;
                case LogStatus.Success:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("[Success] ");
                    break;
                case LogStatus.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("[Warning] ");
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