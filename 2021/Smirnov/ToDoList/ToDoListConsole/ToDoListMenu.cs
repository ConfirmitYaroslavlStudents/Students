using System;

namespace ToDoListConsole
{
    public enum ToDoListMenuEnum
    {
        CreateTask = 1,
        OpenTaskRedactor = 2,
        GetAllTask = 3,
        SaveAndExit = 4
    }
    public class ToDoListMenu
    {
        private static void WriteMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Create task");
            Console.WriteLine("2. Open task redactor");
            Console.WriteLine("3. Get list all task");
            Console.WriteLine("4. Save and exit");
        }

        public static bool WorkWithMenu(ToDoListApp.ToDoListApp  toDoListApp)
        {
            WriteMenu();
            var item = (ToDoListMenuEnum)int.Parse(Console.ReadLine());

            switch(item)
            {
                case (ToDoListMenuEnum.CreateTask):

                    Console.WriteLine("Write name of task");
                    try
                    {
                        toDoListApp.AddTask(Console.ReadLine());
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }

                    break;
                case (ToDoListMenuEnum.OpenTaskRedactor):
                    Console.WriteLine("Write name of task");
                    try
                    {
                        var taskRedactor = new ToDoListApp.TaskRedactor(toDoListApp.GetTask(Console.ReadLine()));
                        while (TaskRedactorMenu.WorkWithMenu(taskRedactor, taskRedactor.TaskName)) ;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.Clear();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                    break;
                case (ToDoListMenuEnum.GetAllTask):
                    foreach(var task in toDoListApp.GetAllTask())
                        Console.WriteLine($@"{task.Name} {task.Description} {task.Status}");
                    Console.WriteLine("Press key...");
                    Console.ReadKey();
                    break;
                case (ToDoListMenuEnum.SaveAndExit):
                    return false;
                default:
                    Console.WriteLine("Incorrect Choice");
                    return false;
            }

            return true;
        }
    }
}
