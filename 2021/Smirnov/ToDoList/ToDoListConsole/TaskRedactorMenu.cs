using System;
using ToDoListApp;

namespace ToDoListConsole
{
    public enum TaskRedactorMenuEnum
    {
        ChangeName = 1,
        ChangeDescription = 2,
        CompleteTask = 3,
        Return = 4
    }
    public class TaskRedactorMenu
    {
        private static void WriteMenu(string taskName = "")
        {
            Console.Clear();
            Console.WriteLine(@$"Task ""{taskName}""");
            Console.WriteLine("1. Change name");
            Console.WriteLine("2. Сhange description");
            Console.WriteLine("3. Complete task");
            Console.WriteLine("4. Return");
        }

        public static bool WorkWithMenu(TaskRedactor taskRedactor, string taskName)
        {
            WriteMenu(taskName);
            var item = (TaskRedactorMenuEnum)int.Parse(Console.ReadLine());

            switch (item)
            {
                case (TaskRedactorMenuEnum.ChangeName):

                    Console.WriteLine("Write new name for task");
                    taskRedactor.ChangeName(Console.ReadLine());
                    break;
                case (TaskRedactorMenuEnum.ChangeDescription):
                    Console.WriteLine("Write new description for task");
                    taskRedactor.ChangeDescription(Console.ReadLine());
                    break;
                case (TaskRedactorMenuEnum.CompleteTask):
                    taskRedactor.CompleteTask();
                    Console.WriteLine($"Task Complete!");
                    Console.WriteLine("Press key...");
                    Console.ReadKey();
                    break;
                case (TaskRedactorMenuEnum.Return):
                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");
                    return false;
            }

            return true;
        }
    }
}
