namespace Mp3TagLib.Operations
{
    public class Redo:Operation
    {
        public const int ID = 2;
        private readonly Operation _lastCanceledOperation;

        public Redo(OperationNode lastOperation)
        {
            OperationId = ID;
            if (lastOperation.Next != null)
            _lastCanceledOperation = lastOperation.Next.Value;

        }
        public override void Call()
        {
            if(_lastCanceledOperation==null)
                return;
            _lastCanceledOperation.Call();
        }

        public override void Cancel()
        {
            if (_lastCanceledOperation == null)
                return;
            _lastCanceledOperation.Cancel();
        }
    }
}
