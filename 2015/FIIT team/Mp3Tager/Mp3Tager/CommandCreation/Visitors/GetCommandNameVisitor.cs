using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCreation
{
    public class GetCommandNameVisitor: ICommandVisitor<string>
    {
        public string Visit(RenameCommand command)
        {
            return CommandNames.Rename;
        }

        public string Visit(ChangeTagsCommand command)
        {
            return CommandNames.ChangeTags;
        }

        public string Visit(AnalyseCommand command)
        {
            return CommandNames.Analyse;
        }

        public string Visit(SyncCommand command)
        {
            return CommandNames.Sync;
        }
    }
}
