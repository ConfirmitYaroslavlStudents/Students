using System;

namespace FaultTolerance.Extensions.Wrap
{
    public class WrapBuilder : ToleranceBuilder
    {
        private readonly IToleranceBuilder _outer;
        private readonly IToleranceBuilder _inner;

        private bool _unwrap;

        public WrapBuilder(IToleranceBuilder outer, IToleranceBuilder inner)
        {
            _outer = outer;
            _inner = inner;
        }

        public WrapBuilder UnWrap()
        {
            _unwrap = true;

            return this;
        }

        public override void Execute(Action action)
        {
            if (_unwrap)
            {
                _inner.Execute(action);
            }
            else
            {
                new WrapRunner(_outer, _inner).Execute(action);
            }
        }
    }
}
