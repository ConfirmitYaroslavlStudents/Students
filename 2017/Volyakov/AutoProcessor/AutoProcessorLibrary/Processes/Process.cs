using System.Collections.Generic;

namespace AutoProcessor
{
    public class Process
    {
        public Process NextProcess { get; set; }

        public StepCollection Steps { get; set; }

        public Status ProcessStatus { get; protected set; }

        public Process()
        {
            Steps = new StepCollection();

            NextProcess = null;

            ProcessStatus = Status.NotStarted;
        }

        public Process(IEnumerable<IStep> steps,Process nextProcess = null)
        {
            Steps = new StepCollection(steps);

            NextProcess = nextProcess;

            ProcessStatus = Status.NotStarted;
        }
        
        public virtual void Start()
        {
            ProcessStatus = Status.Launched;

            StepProcessor.Start(Steps);

            if (AllStepsFinishedWithError())
                ProcessStatus = Status.Error;
            else
                ProcessStatus = Status.Finished;
        }

        private bool AllStepsFinishedWithError()
        {
            if (Steps.Count == 0)
                return false;

            foreach(StepStatusPair pair in Steps)
            {
                if (pair.Status != Status.Error)
                    return false;
            }

            return true;
        }
    }
}
