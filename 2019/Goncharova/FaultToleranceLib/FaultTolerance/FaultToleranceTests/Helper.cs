using System;

namespace FaultToleranceTests
{
    public class Helper
    {
        public int ThrowCount { get; set; }
        public Exception Exception { get; set; }

        public Helper(Exception exception, int throwCount)
        {
            ThrowCount = throwCount;
            Exception = exception;
        }

        public void ThrowException()
        {
            ThrowCount--;

            if (ThrowCount >= 0)
            {
                throw Exception;
            }
        }

    }
}
