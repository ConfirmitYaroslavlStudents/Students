using System.Collections.Generic;
using System.Linq;
using ToDoLibrary.ChainOfResponsibility;
using ToDoLibrary.ChainOfResponsibility.ForToggleStatus;

namespace ToDoLibrary.Commands
{
    public class ToggleCommand: ICommand
    {
        public int Index;
        public StatusTask Status;
        public List<Task> Tasks;

        public void PerformCommand()
        {
            GetChainOfResponsibility().HandlerResponsibility(this);

            Tasks[Index].Status = Status;
        }

        private IHandlerResponsibility GetChainOfResponsibility()
        {
            var correctToggleOfStatusesResponsibility = new CorrectToggleOfStatusesResponsibility();
            var limitOfStatusesToggleResponsibility = new LimitOfStatusesToggleResponsibility();
            var indexInRangeToggleResponsibility = new IndexInRangeToggleResponsibility();

            indexInRangeToggleResponsibility.SetNext(limitOfStatusesToggleResponsibility)
                .SetNext(correctToggleOfStatusesResponsibility);
            
            return indexInRangeToggleResponsibility;
        }
    }
}