using Mp3TagLib.Operations;

namespace Mp3TagTest
{
    public class AnotherTestOperation:Operation
    {
        public const int ID = 4;
        public int CallCount { get; set; }
        public int CancelCount { get; set; }
        public bool CancelFlag { get { return IsCanceled; } }

        public AnotherTestOperation()
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