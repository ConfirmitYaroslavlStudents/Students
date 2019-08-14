using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    internal class StrategyExceptions
    {
        private readonly List<Type> exceptionsHandled = new List<Type>();

        internal void Add(Type exception)
        {
            if (!Contains(exception))
            {
                exceptionsHandled.Add(exception);
            }
        }

        internal bool Contains(Type exception) => exceptionsHandled.Contains(exception);

    }
}