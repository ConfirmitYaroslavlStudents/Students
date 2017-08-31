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

        public void Execute()
        {
            _obj.Changed = true;
        }
    }
}
