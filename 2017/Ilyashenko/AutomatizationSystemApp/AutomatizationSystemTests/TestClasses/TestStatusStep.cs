using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    public class TestStatusStep : IStep
    {
        private TestObject _obj;

        public TestStatusStep(TestObject obj)
        {
            _obj = obj;
        }

        public void Execute(Processor sender, int stepId)
        {
            _obj.Status = Status.Successful;
            sender.StepStatus[stepId] = Status.Successful;
            sender.NextStepId++;
        }
    }
}
