using System;
using System.Threading;

namespace FaultTolerance.Plain
{
    public class PlainTolerance : Tolerance
    {
        public override void Execute(Action<CancellationToken> action) =>
            PlainProcessor.Execute(_ => action(_));
    }
}
