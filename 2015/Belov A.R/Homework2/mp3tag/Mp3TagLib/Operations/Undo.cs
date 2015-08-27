namespace Mp3TagLib.Operations
{
    public class Undo:Operation
    {
        public const int ID = 1;
        private readonly Operation _lastOperation;

        public Undo(OperationNode lastOperation)
        {
            OperationId = ID;
            if (lastOperation != null)
            _lastOperation=lastOperation.Value;
        }
     
        public override void Call()
        {
            if(_lastOperation==null)
                return;
            _lastOperation.Cancel();
        }

        public override void Cancel()
        {
            if (_lastOperation == null)
                return;
            _lastOperation.Call();
        }
    }
}
