namespace Mp3TagLib.Operations
{
    public abstract class AbstractOperationFactory
    {
        public abstract Operation CreateOperation(int id);
        public abstract Operation CreateOperation(string name);

        public Operation Create(string name, OperationNode lastOperationNode)
        {
            switch (name.ToLower())
            {

                case "undo":
                    return Create(Undo.ID, lastOperationNode);
                case "redo":
                    return Create(Redo.ID, lastOperationNode);
                default:
                    int id;
                    if (int.TryParse(name, out id))
                    {
                        return Create(id, lastOperationNode);
                    }
                    return CreateOperation(name);
            }
        }

        public Operation Create(int id, OperationNode lastOperationNode)
        {
            switch (id)
            {

                case Undo.ID:
                    return new Undo(lastOperationNode);
                case Redo.ID:
                    return new Redo(lastOperationNode);
                default:
                    return CreateOperation(id);
            }
        }
    }
}