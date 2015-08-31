namespace CommandCreation
{
    public interface ICommandVisitor<out T>
    {
        T Visit(RenameCommand command);

        T Visit(ChangeTagsCommand command);

        T Visit(AnalyseCommand command);

        T Visit(SyncCommand command);
    }
}
