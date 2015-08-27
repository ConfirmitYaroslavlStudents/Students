using System;
using Mp3TagLib;
using Mp3TagLib.Operations;

namespace mp3tager.Operations
{
    class Exit:Operation
    {
        public const int ID =9;

        public Exit()
        {
            OperationId = ID;
        }
        
        public override void Call()
        {
            Environment.Exit(0);
        }

        public override void Cancel()
        {
            throw new InvalidOperationException("Can't cancel this operation");
        }
    }
}
