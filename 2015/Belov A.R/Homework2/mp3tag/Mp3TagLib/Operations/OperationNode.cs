namespace Mp3TagLib.Operations
{
    public class OperationNode
    {
        public Operation Value { get; set; }
        public OperationNode Next { get; set; }
        public OperationNode Previous { get; set; }
    }
}
