namespace Mp3TagLib.Operations
{
    public abstract class Operation
    {
        public int OperationId { get;protected set; }
        protected bool IsCanceled;
        public abstract void Call();
        public abstract void Cancel();
    }
}
