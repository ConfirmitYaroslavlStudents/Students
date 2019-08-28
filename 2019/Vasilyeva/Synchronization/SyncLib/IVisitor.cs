namespace SyncLib
{
    public interface IVisitor
    {
        void Visit(DifferentContentConflict conflict);
        void Visit(ExistDirectoryConflict conflict);

        void Visit(ExistFileConflict conflict);

        void Visit(NoExistDirectoryConflict conflict);
        void Visit(NoExistFileConflict conflict);

    }
}