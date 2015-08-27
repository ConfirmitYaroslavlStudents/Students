using System;
using Mp3TagLib;
using Mp3TagLib.Operations;

namespace mp3tager.Operations
{
    public class OperationFactory:AbstractOperationFactory
    {
        public override Operation CreateOperation(int id)
        {
            switch (id)
            {
                case Changetags.ID:
                    return new Changetags();
                case Rename.ID:
                    return new Rename();
                case Analysis.ID:
                    return new Analysis();
                case Sync.ID:
                    return new Sync();
                case HandSync.ID:
                    return new HandSync();
                case Exit.ID:
                    return new Exit();
                case LateSync.ID:
                    return new LateSync();
                default:
                    throw new ArgumentException("Invalid command");
            }
        }

        public override Operation CreateOperation(string name)
        {
            switch (name)
            {
                case "changetags":
                    return CreateOperation(Changetags.ID);
                case "rename":
                    return CreateOperation(Rename.ID);
                case "analysis":
                    return CreateOperation(Analysis.ID);
                case "sync":
                    return CreateOperation(Sync.ID);
                case "handsync":
                    return CreateOperation(HandSync.ID);
                case "exit":
                    return CreateOperation(Exit.ID);
                case "latesync":
                    return CreateOperation(LateSync.ID);
                default:
                    throw new ArgumentException("Invalid command");
            }
        }
    }
}
