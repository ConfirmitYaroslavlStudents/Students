using System;

namespace FaultTolerance.Extensions.Wrap
{
    internal class WrapRunner
    {
        private readonly IToleranceBuilder _outer;
        private readonly IToleranceBuilder _inner;

        public WrapRunner(IToleranceBuilder outer, IToleranceBuilder inner)
        {
            _outer = outer;
            _inner = inner;
        }

        public void Execute(Action action)
        {
            _outer.Execute(() => _inner.Execute(action));
        }
    }
}