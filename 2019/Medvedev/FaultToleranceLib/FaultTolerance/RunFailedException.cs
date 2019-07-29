using System;

namespace FaultTolerance
{
    internal class RunFailedException : Exception
    {
        public RunFailedException()
        {
        }

        public RunFailedException(string message)
            : base(message)
        {
        }

        public RunFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}