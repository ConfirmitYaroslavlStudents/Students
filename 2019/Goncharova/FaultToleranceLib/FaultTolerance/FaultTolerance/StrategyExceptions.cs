using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public class StrategyExceptions
    {
        private readonly List<Type> exceptionsHandled = new List<Type>();

        internal void Add(Exception exception)
        {
            if (!Contains(exception))
            {
                exceptionsHandled.Add(exception.GetType());
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
            return exceptionsHandled.Contains(exception.GetType());
        }

    }
}