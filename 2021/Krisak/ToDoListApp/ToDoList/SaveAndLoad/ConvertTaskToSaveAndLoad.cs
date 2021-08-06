using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToDoLibrary
{
    public static class ConvertTaskToSaveAndLoad
    {
        private const string StringWhenStatusToDo = "=ToDo";
        private const string StringWhenStatusInProgress = "=InProgress";
        private const string StringWhenStatusDone = "=Done";

        public static IEnumerable<Task> ConvertLinesAfterLoading(IEnumerable<string> lines)
        {
            return lines.Select(LineToTask).ToList();
        }

        public static IEnumerable<string> ConvertNoteToSave(IEnumerable<Task> notes)
        {
            return notes.Select(NoteToLine).ToList();
        }

        private static Task LineToTask(string line)
        {
            var regex = new Regex($"{StringWhenStatusToDo}$|{StringWhenStatusDone}$|{StringWhenStatusInProgress}$");

            var status = regex.Match(line).Value.ToString();
            var text = regex.Split(line)[0];

            return status switch
            {
                StringWhenStatusToDo => new Task {Text = text, Status = StatusTask.ToDo},
                StringWhenStatusInProgress => new Task() { Text = text, Status = StatusTask.IsProgress},
                StringWhenStatusDone => new Task {Text = text, Status = StatusTask.Done},
                _ => new Task {Text = line, Status = StatusTask.ToDo}
            };
        }

        private static string NoteToLine(Task task)
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
