using System;
using System.Collections.Generic;
using System.Linq;

namespace FaultTolerance.Extensions.Fallback
{
    internal class FallbackRunner
    {
        private readonly List<Func<Exception, bool>> _knownExceptionPredicates;
        private readonly Action _fallback;

        public FallbackRunner(List<Func<Exception, bool>> knownExceptionPredicates, Action fallback)
        {
            _knownExceptionPredicates = knownExceptionPredicates ?? new List<Func<Exception, bool>>();
            _fallback = fallback;
        }

        internal void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                var knownException = _knownExceptionPredicates.FirstOrDefault(x => x(ex));
                if (knownException == null)
                {
                    throw ex;
                }

                _fallback();
            }
        }
    }
}