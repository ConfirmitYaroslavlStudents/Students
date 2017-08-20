using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    public class TestStep : IStep
    {
        private TestObject _obj;

        public TestStep(TestObject obj)
        {
            _obj = obj;
        }

        public void Execute(Processor sender, int stepId)
        {
            _obj.Changed = true;
            sender.StepStatus[stepId] = Status.Successful;
            sender.NextStepId++;
        }
    }
}
