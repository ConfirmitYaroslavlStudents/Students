using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToDoLibrary.SaveAndLoad
{
    public static class TasksToSaveAndLoadConverter
    {
        private const string StringWhenStatusToDo = "=ToDo";
        private const string StringWhenStatusInProgress = "=InProgress";
        private const string StringWhenStatusDone = "=Done";

        public static List<Task> ConvertTasksAfterLoading(IEnumerable<string> lines)
        {
            return lines.Select(LineToTask).ToList();
        }

        public static IEnumerable<string> ConvertTasksToSave(IEnumerable<Task> tasks)
        {
            return tasks.Select(TaskToLine).ToList();
        }

        private static Task LineToTask(string line)
        {
            var regex = new Regex($"{StringWhenStatusToDo}$|{StringWhenStatusDone}$|{StringWhenStatusInProgress}$");

            var status = regex.Match(line).Value;
            var text = regex.Split(line)[0];

            return status switch
            {
                StringWhenStatusToDo => new Task {Text = text, Status = StatusTask.ToDo},
                StringWhenStatusInProgress => new Task() { Text = text, Status = StatusTask.IsProgress},
                StringWhenStatusDone => new Task {Text = text, Status = StatusTask.Done},
                _ => new Task {Text = line, Status = StatusTask.ToDo}
            };
        }

        private static string TaskToLine(Task task)
        {
            return task.Status switch
            {
                StatusTask.ToDo => task.Text + StringWhenStatusToDo,
                StatusTask.IsProgress => task.Text + StringWhenStatusInProgress,
                StatusTask.Done => task.Text + StringWhenStatusDone
            };
        }
    }
}
