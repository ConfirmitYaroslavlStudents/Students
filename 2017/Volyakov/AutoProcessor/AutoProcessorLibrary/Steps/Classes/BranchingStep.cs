using System.Collections.Generic;

namespace AutoProcessor
{
    public class BranchingStep : Step
    {
        private Step _flagStep;
        private List<Step> _stepsIfFinished;
        private List<Step> _stepsIfError;

        public BranchingStep (Step flagStep, List<Step> stepsTakenIfFinished,List<Step> stepsTakenIfError)
        {
            StepStatus = Status.NotStarted;

            _flagStep = flagStep;

            _stepsIfFinished = stepsTakenIfFinished;

            _stepsIfError = stepsTakenIfError;
        }

        public override void Start()
        {
            if (_flagStep.StepStatus == Status.NotStarted || _flagStep.StepStatus == Status.Launched)
            {
                StepStatus = Status.Error;
                return;
            }

            StepStatus = Status.Launched;

            if(_flagStep.StepStatus == Status.Finished)
            {
                foreach (var step in _stepsIfFinished)
                    step.Start();
            }
            else
            {
                foreach (var step in _stepsIfError)
                    step.Start();
            }

            StepStatus = Status.Finished;
        }
    }
}
