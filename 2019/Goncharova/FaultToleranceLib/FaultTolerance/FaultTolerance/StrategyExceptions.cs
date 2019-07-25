using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public class StrategyExceptions
    {
        private readonly List<Exception> exceptionsHandled = new List<Exception>();

        internal void Add(Exception exception)
        {
            if (!Contains(exception))
            {
                exceptionsHandled.Add(exception);
            }
        }

        internal void AddMany(List<Exception> exceptions)
        {
            foreach (var ex in exceptions)
            {
                Add(ex);
            }
        }

        internal bool Contains(Exception exception)
        {
            return exceptionsHandled.Contains(exception);
        }

    }
}