﻿using System;
using System.Threading;

namespace FaultTolerance.Retry
{
    public class RetryTolerance : Tolerance
    {
        private readonly int permittedRetryCount;
        private readonly ToleranceExceptions configuredExceptions;

        public RetryTolerance(ToleranceBuilder ToleranceBuilder, int retryCount)
        {
            configuredExceptions = ToleranceBuilder.configuredExceptions;
            if (retryCount < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(retryCount)} should be non negative");
            }
            permittedRetryCount = retryCount;
        }

        public override void Execute(Action<CancellationToken> action)
            => RetryProcessor.Execute(_ => action(_),
                                      configuredExceptions,
                                      permittedRetryCount);
    }

}
