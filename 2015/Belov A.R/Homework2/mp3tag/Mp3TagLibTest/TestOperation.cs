using Mp3TagLib.Operations;

namespace Mp3TagTest
{
    public class TestOperation:Operation
    {
        public const int ID = 3;
        public int CallCount { get; set; }
        public int CancelCount { get; set; }
        public bool CancelFlag { get { return IsCanceled; } }

        public TestOperation()
        {
            OperationId = ID;
        }
        public override void Call()
        {
            CallCount++;
        }

        public override void Cancel()
        {
            IsCanceled = true;
            CancelCount++;
        }
    }
}