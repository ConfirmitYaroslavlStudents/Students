using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility.ForToggleStatus
{
    public class IndexInRangeToggleResponsibility: AbstractHandlerResponsibility
    {
        public override object HandlerResponsibility(object @object)
        {
            var command = (ToggleCommand) @object;

            if (command.Index<0|| command.Index>=command.Tasks.Count)
                throw new WrongEnteredCommandException($"Task not found with number {command.Index + 1}.");

            return base.HandlerResponsibility(@object);
        }
    }
}