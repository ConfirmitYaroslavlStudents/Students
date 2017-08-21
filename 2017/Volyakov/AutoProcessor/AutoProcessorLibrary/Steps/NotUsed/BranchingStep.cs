using System.Collections.Generic;

namespace AutoProcessor
{
    //public class BranchingStep : IStep
    //{
    //    private IStep _flagStep;
    //    private List<IStep> _stepsIfFinished;
    //    private List<IStep> _stepsIfError;

    //    public BranchingStep (IStep flagStep, List<IStep> stepsTakenIfFinished,List<IStep> stepsTakenIfError)
    //    {
    //        StepStatus = Status.NotStarted;

    //        _flagStep = flagStep;

    //        _stepsIfFinished = stepsTakenIfFinished;

    //        _stepsIfError = stepsTakenIfError;
    //    }

    //    public override void Start()
    //    {
    //        if (_flagStep.StepStatus == Status.NotStarted || _flagStep.StepStatus == Status.Launched)
    //        {
    //            StepStatus = Status.Error;
    //            return;
    //        }

    //        StepStatus = Status.Launched;

    //        if(_flagStep.StepStatus == Status.Finished)
    //        {
    //            foreach (var step in _stepsIfFinished)
    //                step.Start();
    //        }
    //        else
    //        {
    //            foreach (var step in _stepsIfError)
    //                step.Start();
    //        }

    //        StepStatus = Status.Finished;
    //    }
    //}
}
