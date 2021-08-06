using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility.ForToggleStatus
{
    public class CorrectToggleOfStatusesResponsibility: AbstractHandlerResponsibility
    {
        public override object HandlerResponsibility(object @object)
        {
            var command = (ToggleCommand) @object;

            return command.Status switch
            {
                StatusTask.IsProgress when command.Tasks[command.Index].Status != StatusTask.ToDo => throw
                    new WrongEnteredCommandException(
                        "Status \"In Progress\" can only be toggled from status \"To Do\"."),

                StatusTask.Done when command.Tasks[command.Index].Status != StatusTask.IsProgress => throw
                    new WrongEnteredCommandException(
                        "Status \"Done\" can only be toggled from status \"In Progress\"."),

                _ => base.HandlerResponsibility(@object)
            };
        }
    }
}