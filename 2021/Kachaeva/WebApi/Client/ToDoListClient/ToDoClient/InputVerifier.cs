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

        public static int GetValidTaskNumber(string taskNumberInput)
        {
            if (!int.TryParse(taskNumberInput, out int taskNumber))
                throw new ArgumentException("Нужно ввести число");
            return taskNumber;
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
