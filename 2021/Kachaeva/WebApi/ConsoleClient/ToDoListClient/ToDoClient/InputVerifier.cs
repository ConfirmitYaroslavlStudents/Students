using System;

namespace ToDoClient
{
    public class InputVerifier
    {
        public static string GetValidTaskText(string taskTextInput)
        {
            if (String.IsNullOrEmpty(taskTextInput))
                throw new ArgumentException("Нельзя добавить пустое задание");
            return taskTextInput;
        }

        public static int GetValidTaskId(string taskIdInput)
        {
            if (!int.TryParse(taskIdInput, out int taskId))
                throw new ArgumentException("Нужно ввести число");
            return taskId;
        }

        public static bool GetValidTaskStatus(string taskStatusInput)
        {
            if (taskStatusInput == "true")
                return true;
            if (taskStatusInput == "false")
                return false;
            throw new ArgumentException("Нужно ввести true или false");
        }
    }
}