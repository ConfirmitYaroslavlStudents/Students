using System;
using System.Collections.Generic;

namespace FaultTolerance.Retry
{
    public class RetryStrategy : Strategy
    {
        private int permittedRetryCount;

        public RetryStrategy(Exception exception, int retryCount) : base(exception)
        {
            PermittedRetryCount = retryCount;
        }

        public RetryStrategy(List<Exception> exceptions, int retryCount) : base(exceptions)
        {
            PermittedRetryCount = retryCount;
        }

        private int PermittedRetryCount
        {
            get => permittedRetryCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("RetryCount should be non negative");
                }
                else
                {
                    permittedRetryCount = value;
                }
            }
        }
        public override T Execute<T>(Func<T> action)
            => RetryProcessor.Execute<T>(action, ExceptionsHandled, PermittedRetryCount);
    }

    internal class RetryPolicy<TResult> : Policy<TResult>
    {
        private int permittedRetryCount;
        public RetryPolicy(Exception exception, int retryCount) : base(exception)
        {
            PermittedRetryCount = retryCount;
        }

        public RetryPolicy(List<Exception> exceptions, int retryCount) : base(exceptions)
        {
            PermittedRetryCount = retryCount;
        }

        private int PermittedRetryCount
        {
            get => permittedRetryCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("RetryCount should be non negative");
                }
                else
                {
                    permittedRetryCount = value;
                }
            }
        }

        public override TResult Execute(Func<TResult> action) 
            => RetryProcessor.Execute<TResult>(action, ExceptionsHandled, PermittedRetryCount);
    }
}
