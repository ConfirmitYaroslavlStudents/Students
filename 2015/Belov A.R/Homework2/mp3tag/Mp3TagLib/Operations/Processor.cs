namespace Mp3TagLib.Operations
{
    public class Processor
    {
        protected OperationsList OperationList;
        private readonly AbstractOperationFactory _operationFactory;
        public Processor(AbstractOperationFactory operationFactory)
        {
            OperationList=new OperationsList();
            _operationFactory = operationFactory;
        }

        public void CallOperation(Operation operation)
        {
            operation.Call();
            switch (operation.OperationId)
            {
                case Redo.ID:
                    OperationList.MoveNext();
                    break;
                case Undo.ID:
                    OperationList.MovePrevious();
                    break;
                default:
                    OperationList.Add(operation);
                    break;
            }
        }

        public Operation CreateOperation(string operationName)
        {
            return _operationFactory.Create(operationName, OperationList.Current);
        }
    }
}
