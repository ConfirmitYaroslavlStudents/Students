using System;
using System.Threading;

namespace FaultTolerance
{
    public abstract class Tolerance
    {
        public abstract void Execute(Action<CancellationToken> action);

        public void Execute(Action action) => Execute(_ => action());
    }

}
