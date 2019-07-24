using System;
using System.Collections.Generic;

namespace FaultToleranceLib
{
    public static class FaultTolerance
    {
        // DONE: Probably static methods?
        // DONE: Probably multiple exception support?
        // DONE: Tests for multiple exception methods
        // TODO: Maybe HashSet instead of List?

        public static void Try<E>(Action action, int count) where E : Exception
        {
            Try(new List<Type> { typeof(E) }, action, count);
        }

        public static void Try(List<Type> exceptions, Action action, int count)
        {
            try
            {
                DoAction(exceptions, action, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void TryFallback<E>(Action action, int count, Action fallback) where E : Exception
        {
            var dict = new Dictionary<Type, Action>();
            dict.Add(typeof(E), fallback);
            TryFallback(dict, action, count);
        }

        public static void TryFallback(Dictionary<Type, Action> exceptionFallbacks, Action action, int count)
        {
            try
            {
                DoAction(new List<Type>(exceptionFallbacks.Keys), action, count);
            }
            catch (Exception ex)
            {
                if (exceptionFallbacks.ContainsKey(ex.GetType()))
                {
                    Action fallback = exceptionFallbacks[ex.GetType()];
                    fallback();
                }
                else
                    throw;
            }
        }

        private static void DoAction(List<Type> exceptions, Action action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (exceptions.Contains(ex.GetType()) && i < count - 1)
                        continue;
                    else
                        throw ex;
                }
                break;
            }
        }
    }
}
