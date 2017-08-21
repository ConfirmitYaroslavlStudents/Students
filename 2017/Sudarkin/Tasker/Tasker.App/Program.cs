using System;
using Tasker.Core.Actions.ConsoleCommand;
using Tasker.Core.Actions.WriteLine;
using Tasker.Core.BehaviourTree;

namespace Tasker.App
{
    internal class Program
    {
        private static void Main()
        {
            BehaviourTree positiveBranch = new BehaviourTree.Builder()
                .Do(new WriteLineAction(new WriteLineOptions { Message = "Успех!" }))
                .Do(new WriteLineAction(new WriteLineOptions { Message = "Повторюсь: Успех!" }))
                .Build();

            BehaviourTree negativeBranch = new BehaviourTree.Builder()
                .Do(new WriteLineAction(new WriteLineOptions { Message = "Неуспех!" }))
                .Build();

            BehaviourTree subTree = new BehaviourTree.Builder()
                .Do(new ConsoleCommandAction(new ConsoleOptions { Command = "ping ok.ru" }))
                .Do(new ConsoleCommandAction(new ConsoleOptions { Command = "ping vk.com" }))
                .Build();

            new BehaviourTree.Builder()
                .Do(new ConsoleCommandAction(new ConsoleOptions { Command = "ping google.com" }))
                .Condition(IsVkComEqualsMailRu, positiveBranch, negativeBranch)
                .Do(new ConsoleCommandAction(new ConsoleOptions { Command = "ping yandex.ru" }))
                .Sequence(subTree)
                .Build()
                .Start();

            Console.ReadKey();
        }

        private static bool IsVkComEqualsMailRu()
        {
            return "vk.com" == "mail.ru";
        }
    }
}
