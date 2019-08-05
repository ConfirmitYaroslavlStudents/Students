using System;

namespace FaultTolerance.Plain
{
    public class PlainStrategy : Strategy
    {
        public PlainStrategy() { }

        public override T Execute<T>(Func<T> action)
        {
            return PlainProcessor.Execute<T>(action);
        }
    }
}
