using System;
using System.Collections.Generic;

namespace FaultTolerance.Plain
{
    public class PlainStrategy : Strategy
    {
        public PlainStrategy(Exception exception) : base(exception)
        {
        }

        public PlainStrategy(List<Exception> exceptions) : base(exceptions)
        {
        }

        public override T Execute<T>(Func<T> action)
        {
            return PlainProcessor.Execute<T>(action);
        }
    }
}
