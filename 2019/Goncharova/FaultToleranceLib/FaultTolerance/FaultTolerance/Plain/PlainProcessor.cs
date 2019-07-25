using System;

namespace FaultTolerance.Plain
{
    internal static class PlainProcessor
    {
        internal static TResult Execute<TResult>(Func<TResult> action)
        {
            try
            {
                return action();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
