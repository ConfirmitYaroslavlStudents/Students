using System.Text;

namespace ToDoClient
{
    public struct ToDoCommands
    {
        public const string DisplayToDoList = "list";
        public const string AddTask = "add";
        public const string RemoveTask = "remove";
        public const string ChangeTaskText = "text";
        public const string ToggleTaskStatus = "status";
        public const string Quit = "q";

        public static string GetMenu()
        {
            var menu = new StringBuilder();
            menu.AppendLine("Что вы хотели бы сделать? Введите:");
            menu.AppendLine($"{DisplayToDoList} - просмотреть список");
            menu.AppendLine($"{AddTask} - добавить задание");
            menu.AppendLine($"{RemoveTask} - удалить задание");
            menu.AppendLine($"{ChangeTaskText} - изменить текст задания");
            menu.AppendLine($"{ToggleTaskStatus} - изменить статус задания");
            menu.AppendLine($"{Quit} - выйти");
            return menu.ToString();
        }
    }
}
