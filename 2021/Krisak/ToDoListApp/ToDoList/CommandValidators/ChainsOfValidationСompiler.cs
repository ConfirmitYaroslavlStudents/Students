using System.Collections.Generic;
using ToDoLibrary.Commands;
using ToDoLibrary.CommandValidators.ValidatorForCommands;

namespace ToDoLibrary.CommandValidators
{
    public static class ChainsOfValidationСompiler
    {
        public static AbstractValidator CompileForAddCommand(AddCommand command)
        {
            var taskLengthLimit = new TaskLengthLimitValidator(false, 50,command);
            return taskLengthLimit;
        }
        
        public static AbstractValidator CompileForEditCommand(List<Task> tasks, EditTextCommand textCommand)
        {
            var indexInRange = new ExistenceIdValidator(false, tasks, textCommand);
            var taskLengthLimit = new TaskLengthLimitValidator(false, 50,textCommand);

            indexInRange.SetNext(taskLengthLimit);

            return indexInRange;
        }

        public static AbstractValidator CompileForToggleCommand(List<Task> tasks, ToggleStatusCommand command)
        {
            var indexInRange = new ExistenceIdValidator(true, tasks,command);
            var correctToggleSequence = new CorrectToggleSequenceValidator(false, tasks,command);
            var limitOfStatuses = new LimitOfStatusesValidator(false, tasks, command);

            indexInRange.SetNext(correctToggleSequence).SetNext(limitOfStatuses);
            
            return indexInRange;
        }

        public static AbstractValidator CompileForDeleteCommand(List<Task> tasks,DeleteCommand command)
        {
            var indexInRange = new ExistenceIdValidator(false, tasks,command);

            return indexInRange;
        }

        public static AbstractValidator CompileForRollbackCommand(int rollbackCount,StartRollbackCommand command)
        {
            var countRollback = new CountRollbackInRangeValidator(false, rollbackCount, command);

            return countRollback;
        }
    }
}