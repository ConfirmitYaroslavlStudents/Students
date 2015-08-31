namespace CommandCreation
{
    public class GetFilePathVisitor: ICommandVisitor<string>
    {
        public string Visit(RenameCommand command)
        {
            return command.File.FullName;
        }

        public string Visit(ChangeTagsCommand command)
        {
            return command.File.FullName;
        }

        public string Visit(AnalyseCommand command)
        {
            return command.File.FullName;
        }

        public string Visit(SyncCommand command)
        {
            return command.File.FullName;
        }
    }
}
