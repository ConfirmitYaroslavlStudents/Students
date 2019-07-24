using System;

namespace FaultToleranceLib
{
    public static class FaultTolerance
    {
        // DONE: Probably static methods?
        // TODO: Probably multiple exception support?
        private static void DoAction(Type exceptionType, Action action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == exceptionType && i < count - 1)
                        continue;
                    else
                        throw ex;
                }
                break;
            }
        }

        public static void Try(Type exceptionType, Action action, int count)
        {
            try
            {
                DoAction(exceptionType, action, count);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public static void Try<E>(Action action, int count) where E : Exception
        {
            Try(typeof(E), action, count);
        }

        public static void TryFallback(Type exceptionType, Action action, int count, Action fallback)
        {
            try
            {
                DoAction(exceptionType, action, count);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == exceptionType)
                    fallback();
                else
                    throw;
            }
        }

        public static void TryFallback<E>(Action action, int count, Action fallback) where E : Exception
        {
            TryFallback(typeof(E), action, count, fallback);
        }
    }
}
