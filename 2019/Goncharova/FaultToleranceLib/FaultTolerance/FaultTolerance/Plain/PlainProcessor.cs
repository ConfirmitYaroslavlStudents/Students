using System;
using System.Threading;

namespace FaultTolerance.Plain
{
    internal static class PlainProcessor
    {
        internal static void Execute(Action<CancellationToken> action)
        {
            try
            {
                action(CancellationToken.None);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
