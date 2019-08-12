using System;
using System.Collections.Generic;

namespace FaultTolerance.Extensions.Fallback
{
    public class FallbackBuilder : ToleranceBuilder
    {
        private readonly List<Func<Exception, bool>> _knownExceptionPredicates = new List<Func<Exception, bool>>();
        private Action _fallback;

        public FallbackBuilder Or<TException>()
        {
            _knownExceptionPredicates.Add(ex => ex is TException);

            return this;
        }

        public FallbackBuilder WithFallback(Action fallback)
        {
            _fallback = fallback;

            return this;
        }

        public override void Execute(Action action)
        {
            new FallbackRunner(_knownExceptionPredicates, _fallback).Execute(action);
        }
    }
}
