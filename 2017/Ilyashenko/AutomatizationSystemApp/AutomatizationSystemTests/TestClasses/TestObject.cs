using AutomatizationSystemLib;

namespace AutomatizationSystemTests
{
    public class TestObject
    {
        public bool Changed { get; set; }
        public Status Status { get; set; }

        public TestObject()
        {
            Status = Status.Waiting;
        }
    }
}
