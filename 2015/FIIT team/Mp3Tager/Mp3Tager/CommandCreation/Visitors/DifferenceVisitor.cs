using System;

namespace CommandCreation
{
    public class DifferenceVisitor : ICommandVisitor<string>
    {
        public string Visit(RenameCommand command)
        {
            return command.OldFullName + "---->" + command.File.FullName + "\n";
        }

        public string Visit(ChangeTagsCommand command)
        {
            return command.OldTags + "---->" + command.File.Tags + "\n";
        }

        public string Visit(AnalyseCommand command)
        {
            return String.Empty;
        }

        public string Visit(SyncCommand command)
        {
            return command.GeneralCommand.Accept(this);
        }
    }
}
