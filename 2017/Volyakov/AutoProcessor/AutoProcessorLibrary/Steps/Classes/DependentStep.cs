namespace AutoProcessor
{
    public class DependentStep : Step
    {
        private Step _mainStep;
        private Step _flagStep;

        public DependentStep(Step mainStep, Step flagStep)
        {
            _mainStep = mainStep;

            _flagStep = flagStep;
        }

        public override void Start()
        {
            try
            {
                if (_flagStep.StepStatus == Status.Finished)
                {
                    StepStatus = Status.Launched;
                    _mainStep.Start();
                }

                StepStatus = _mainStep.StepStatus;
            }
            catch
            {
                StepStatus = Status.Error;
            }
        }
    }
}
