using System.Collections.Generic;
using ToDoLibrary.ChainOfResponsibility.ForToggleStatus;
using ToDoLibrary.Commands;

namespace ToDoLibrary.ChainOfResponsibility
{
    public static class ChainsOfValidationСompiler
    {
        public static AbstractValidator CompileForAddCommand()
        {
            var taskLengthLimit = new TaskLengthLimitValidator(false, 50);
            return taskLengthLimit;
        }
        
        public static AbstractValidator CompileForEditCommand(List<Task> tasks)
        {
            var indexInRange = new IndexInRangeValidator(false, tasks);
            var taskLengthLimit = new TaskLengthLimitValidator(false, 50);

            indexInRange.SetNext(taskLengthLimit);

            return indexInRange;
        }

        public static AbstractValidator CompileForToggleCommand(List<Task> tasks)
        {
            var indexInRange = new IndexInRangeValidator(true, tasks);
            var correctToggleSequence = new CorrectToggleSequenceValidator(false, tasks);
            var limitOfStatuses = new LimitOfStatusesValidator(false, tasks);

            indexInRange.SetNext(correctToggleSequence).SetNext(limitOfStatuses);
            
            return indexInRange;
        }

        public static AbstractValidator CompileForDeleteCommand(List<Task> tasks)
        {
            var indexInRange = new IndexInRangeValidator(false, tasks);

            return indexInRange;
        }

        public static AbstractValidator CompileForRollbackCommand(Stack<ICommand> rollbacks)
        {
            var countRollback = new CountRollbackInRangeValidator(false, rollbacks);

            return countRollback;
        }
    }
}