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

            TriggerApplet trigger = new TriggerApplet(
                State.Successful, new ConsoleCommandApplet(new ConsoleOptions { Command = "ping google.com" }));
            trigger.AddPositiveApplet(new WriteLineApplet(new WriteLineOptions { Message = "Успех!" }));
            trigger.AddPositiveApplet(new WriteLineApplet(new WriteLineOptions { Message = "Повторюсь: Успех!" }));
            trigger.AddNegativeApplet(new WriteLineApplet(new WriteLineOptions { Message = "Неуспех!" }));

            processor.AddApplet(
                new ConsoleCommandApplet(new ConsoleOptions { Command = "ping yandex.ru" }));
            processor.AddApplet(
                new ConsoleCommandApplet(new ConsoleOptions { Command = "ping vk.com" }));
            processor.AddApplet(trigger);
            processor.Start();

            Console.ReadKey();
        }
    }
}
