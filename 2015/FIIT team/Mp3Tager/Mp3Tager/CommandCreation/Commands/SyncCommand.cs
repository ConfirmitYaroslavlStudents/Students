using System;
using FileLib;

namespace CommandCreation
{
    public class SyncCommand: Command
    {
        internal IMp3File File;
        private readonly string _mask;
        internal Command GeneralCommand;

        public SyncCommand(IMp3File mp3File, string mask)
        {
            File = mp3File;
            _mask = mask;
        }

        public override void Execute()
        {
            try
            {
                GeneralCommand = new ChangeTagsCommand(File, _mask);
                GeneralCommand.Execute();
            }
            catch (Exception)
            {
                GeneralCommand = new RenameCommand(File, _mask);
                GeneralCommand.Execute();
            }
        }

        public override void Undo()
        {
            GeneralCommand.Undo();
        }

        public override T Accept<T>(ICommandVisitor<T> visitor)
        {
         return visitor.Visit(this);
        }

        public override bool IsPlanningCommand()
        {
            return true;
        }
    }
}
