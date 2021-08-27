using System;
using ToDoLibrary;
using ToDoLibrary.Commands;
using TodoWeb.Requests;

namespace TodoWeb
{
    public class CommandCreator
    {
        public ICommand CreateCommandForPostRequest(PostRequest postRequest)
        {
            var task = new Task { Text = postRequest.Text };
            if (Enum.TryParse<StatusTask>(postRequest.Status, out var result))
                task.Status = result;

            return new AddCommand { NewTask = task };
        }

        public ICommand CreateCommandForPatchRequest(PatchRequest patchRequest)
        {
            var command = new EditTaskCommand();
            if (patchRequest.Text != null)
                command.EditTextCommand = new EditTextCommand { TaskId = patchRequest.TaskId, Text = patchRequest.Text };

            if (Enum.TryParse<StatusTask>(patchRequest.Status, out var result))
                command.ToggleStatusCommand = new ToggleStatusCommand { TaskId = patchRequest.TaskId, Status = result };

            return command;
        }

        public ICommand CreateCommandForDeleteRequest(DeleteRequest deleteRequest)
        {
            return new DeleteCommand { TaskId = deleteRequest.TaskId };
        }
    }
}