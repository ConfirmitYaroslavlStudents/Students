using System;
using ToDoListLib;

namespace ToDoListConsole
{
    public class ConsoleMenu
    {

        private readonly IReader _reader;
        public readonly ToDoList ToDoList;

        public ConsoleMenu(ToDoList toDoList, IReader reader)
        {
            ToDoList = toDoList;
            _reader = reader;
        }

        public bool StartMenu()
        {
            try
            {
                var selectedAction = _reader.GetSelectedAction();

                if (selectedAction == ToDoListMenuEnum.SaveAndExit)
                    return false;

                var action = GetAction(selectedAction);

                if (action == null)
                {
                    Console.WriteLine("Incorrect command");
                    return true;
                }

                action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine(".....................");
            }

            return true;
        }

        private protected Action GetAction(ToDoListMenuEnum selectedAction)
        {
            return selectedAction switch
            {
                ToDoListMenuEnum.CreateTask => CreateTask,
                ToDoListMenuEnum.DeleteTask => DeleteTask,
                ToDoListMenuEnum.ChangeDescription => ChangeDescription,
                ToDoListMenuEnum.CompleteTask => CompleteTask,
                ToDoListMenuEnum.WriteAllTask => WriteAllTask,
                _ => null
            };
        }
        private void CreateTask()
        {
            Console.WriteLine("Write description of task");
            ToDoList.Add(new Task() { Description = _reader.GetDescription() });
        }
        private void ChangeDescription()
        {
            Console.WriteLine("Write new description for task");
            SelectTask().Description = _reader.GetDescription();
        }
        private protected void DeleteTask()
        {
            ToDoList.Remove(SelectTask());
        }
        public void CompleteTask()
        {
            SelectTask().Status = TaskStatus.Done;
            Console.WriteLine($"Task Complete!");
        }
        private protected Task SelectTask()
        {
            return ToDoList[_reader.GetNumberTask()];
        }
        private protected void WriteAllTask()
        {
            var index = 1;
            foreach (var task in ToDoList)
                Console.WriteLine($@"{index++}. {task.Description} {task.Status}");
        }
    }
}
