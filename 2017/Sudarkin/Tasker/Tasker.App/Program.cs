using System;
using Tasker.Core;
using Tasker.Core.Applets;
using Tasker.Core.Options;

namespace Tasker.App
{
    internal class Program
    {
        private static void Main()
        {
            Processor processor = new Processor();

            processor.AddApplet(
                new ConsoleCommandApplet(new ConsoleOptions { Command = "ping yandex.ru" }));
            processor.AddApplet(
                new ConsoleCommandApplet(new ConsoleOptions { Command = "ping vk.com" }));

            processor.Start();
        }
    }
}
