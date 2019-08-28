namespace SyncLib
{
    public interface IConflict
    {
        void Accept(IVisitor visitor);
    }
}