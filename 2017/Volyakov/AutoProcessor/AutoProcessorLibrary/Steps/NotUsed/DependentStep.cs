namespace AutoProcessor
{
    //public class DependentStep : IStep
    //{
    //    private IStep _mainStep;
    //    private IStep _flagStep;

    //    public DependentStep(IStep mainStep, IStep flagStep)
    //    {
    //        _mainStep = mainStep;

    //        _flagStep = flagStep;
    //    }

    //    public override void Start()
    //    {
    //        try
    //        {
    //            if (_flagStep.StepStatus == Status.Finished)
    //            {
    //                StepStatus = Status.Launched;
    //                _mainStep.Start();
    //            }

    //            StepStatus = _mainStep.StepStatus;
    //        }
    //        catch
    //        {
    //            StepStatus = Status.Error;
    //        }
    //    }
    //}
}
