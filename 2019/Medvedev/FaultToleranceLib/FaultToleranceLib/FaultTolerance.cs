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
            if (count <= 0)
                throw new ArgumentException("Count must be positive");

            Try(new List<Type> { typeof(E) }, action, count);
        }

        public static void Try(List<Type> exceptions, Action action, int count)
        {
            if (!IsDerivedFromException(exceptions))
                throw new ArgumentException("All types in exceptions must be derived from Exception");
            if (count <= 0)
                throw new ArgumentException("Count must be positive");

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
            if (count <= 0)
                throw new ArgumentException("Count must be positive");

            var dict = new Dictionary<Type, Action>();
            dict.Add(typeof(E), fallback);
            TryFallback(dict, action, count);
        }

        public static void TryFallback(Dictionary<Type, Action> exceptionFallbacks, Action action, int count)
        {
            if (!IsDerivedFromException(exceptionFallbacks.Keys))
                throw new ArgumentException("All keys in exceptionFallbacks must be derived from Exception");
            if (count <= 0)
                throw new ArgumentException("Count must be positive");

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

        private static bool IsDerivedFromException(IEnumerable<Type> types)
        {
            foreach (var type in types)
                if (!type.IsSubclassOf(typeof(Exception)))
                    return false;
            return true;
        }
    }
}
