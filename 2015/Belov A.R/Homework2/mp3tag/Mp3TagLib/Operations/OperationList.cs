namespace Mp3TagLib.Operations
{
    public class OperationsList
    {
        public  OperationNode First { get; set; }
        public OperationNode Last { get; set; }
        public OperationNode Current { get; set; }

        public OperationsList()
        {
            First = new OperationNode();
            Last = First;
            First.Next = Last;
            Current = Last;
        }

        public void Add(Operation operation)
        {
            Current.Next = new OperationNode() {Value = operation, Previous = Current};
            Last = Current.Next;
            Current = Last;
        }

        public void MoveNext()
        {
            if (Current.Next != null)
            Current = Current.Next;
        }

        public void MovePrevious()
        {
            if(Current.Previous!=null)
            Current = Current.Previous;
        }

    }
}

