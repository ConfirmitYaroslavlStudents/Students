using System;
using System.Collections.Generic;
using System.Linq;

namespace FaultTolerance
{
    public class Fallback : FaultTolerance
    {
        public Fallback Handle<TException>() where TException : Exception
        {
            validExceptions.Add(ex => ex is TException);

            return this;
        }

        private readonly List<Predicate<Exception>> validExceptions = new List<Predicate<Exception>>();
        private Action fallback;

        public Fallback SetFallback(Action fallback)
        {
            this.fallback = fallback;

            return this;
        }

        public override void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                var known = validExceptions.FirstOrDefault(x => x(e));

                if (known != null)
                    fallback?.Invoke();
                else throw;
            }
        }

    }
}
