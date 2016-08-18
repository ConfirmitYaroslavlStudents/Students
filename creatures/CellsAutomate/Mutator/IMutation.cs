namespace CellsAutomate.Mutator
{
    public interface IMutation
    {
        void Transform();
        void Undo();
    }
}