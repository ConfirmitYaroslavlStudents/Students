using System;
using System.Threading;

namespace FaultTolerance.Plain
{
    public class PlainStrategy : Strategy
    {
        public PlainStrategy() { }

        public override void Execute(Action<CancellationToken> action) => 
            PlainProcessor.Execute<object>((ct) => { action(ct); return null; });
    }
}
