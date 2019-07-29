using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public class ExceptionHandlerParameters
    {
        internal int NumberOfTries
        {
            get => _numberOfTries;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Number of tries must be positive");

                _numberOfTries = value;
            }
        }

        internal Dictionary<Type, Action> Fallbacks { get; } = new Dictionary<Type, Action>();

        private int _numberOfTries;

        public ExceptionHandlerParameters(int numberOfTries)
        {
            NumberOfTries = numberOfTries;
        }

        public ExceptionHandlerParameters Handle<TException>(Action fallback)
            where TException : Exception
        {
            if (Fallbacks.ContainsKey(typeof(TException)))
                throw new ArgumentException("Fallback for this case is already exists");

            Fallbacks.Add(typeof(TException), fallback);

            return this;
        }

        public ExceptionHandlerParameters Handle<TException>()
            where TException : Exception
        {
            Handle<TException>(() => { });

            return this;
        }

        public ExceptionHandlerParameters HandleFailedRun(Action fallback)
        {
            return Handle<RunFailedException>(fallback);
        }
    }
}