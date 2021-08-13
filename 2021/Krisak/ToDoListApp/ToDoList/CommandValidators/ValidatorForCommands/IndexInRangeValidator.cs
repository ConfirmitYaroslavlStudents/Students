using System.Collections.Generic;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public class IndexInRangeValidator: AbstractValidator
    {
        private List<Task> _tasks;

        public IndexInRangeValidator(bool isAbort, List<Task> tasks) : base(isAbort)
        {
            _tasks = tasks;
        }

        public override void Validate(ICommand command)
        {
            var index = GetIndex( command);

            if (index < 0 || index>= _tasks.Count) 
            {
                base.Exceptions.Add($"Task not found with number {index + 1}.");
                if (base.IsAbort)
                    return;
            }
            
            base.Validate( command);
        }

        private int GetIndex(ICommand command)
        {
            return command switch
            {
                EditCommand editCommand => editCommand.Index,
                ToggleCommand toggleCommand=>toggleCommand.Index,
                DeleteCommand deleteCommand=>deleteCommand.Index,
                _ => 0
            };
        }
    }
}