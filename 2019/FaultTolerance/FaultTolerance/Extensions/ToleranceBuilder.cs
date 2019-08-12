using System;
using FaultTolerance.Extensions.Wrap;

namespace FaultTolerance.Extensions
{
    public abstract class ToleranceBuilder : IToleranceBuilder
    {
        public WrapBuilder Wrap(IToleranceBuilder inner)
        {
            return new WrapBuilder(this, inner);
        }

        public IToleranceBuilder WithDecorator<TDecorator>(Func<IToleranceBuilder, TDecorator> creator)
        where TDecorator : IToleranceBuilder
        {
            return creator(this);
        }

        public abstract void Execute(Action action);
    }
}
