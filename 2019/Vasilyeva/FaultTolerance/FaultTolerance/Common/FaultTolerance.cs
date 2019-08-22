using System;

namespace FaultTolerance
{
    public abstract class FaultTolerance : IToleranceBuilder
    {
        public abstract void Execute(Action action);

        public Wrap Wrap(IToleranceBuilder inner)
        {
            return new Wrap(this, inner);
        }
    }
}
