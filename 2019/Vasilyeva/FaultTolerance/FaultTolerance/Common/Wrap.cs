using System;

namespace FaultTolerance
{
    public class Wrap : IToleranceBuilder
    {
        private readonly IToleranceBuilder f;
        private readonly IToleranceBuilder g;
        public Wrap(IToleranceBuilder outer, IToleranceBuilder inner)
        {
            f = outer;
            g = inner;
        }

        public void Execute(Action action)
        {
            f.Execute(() => g.Execute(action));
        }
    }
}