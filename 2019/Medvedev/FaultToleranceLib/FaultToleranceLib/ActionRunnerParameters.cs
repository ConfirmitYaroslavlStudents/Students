using System;
using System.Collections.Generic;

namespace FaultToleranceLib
{
    public class ActionRunnerParameters
    {
        internal int Timeout
        {
            get => _timeout;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Timeout must be positive");

                _timeout = value;
            }
        }

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
        internal bool WithTimeout { get;  }

        internal Dictionary<Type, Action> Fallbacks { get; } = new Dictionary<Type, Action>();

        private int _timeout;
        private int _numberOfTries;

        public ActionRunnerParameters(int numberOfTries) 
        {
            NumberOfTries = numberOfTries;
        }

        public ActionRunnerParameters(int numberOfTries, int timeout) 
            : this(numberOfTries)
        {
            Timeout = timeout;
            WithTimeout = true;
        }

        public ActionRunnerParameters Handle<TException>(Action fallback)
            where TException : Exception
        {
            if (Fallbacks.ContainsKey(typeof(TException)))
                throw new ArgumentException("Fallback for exception is already exists");

            Fallbacks.Add(typeof(TException), fallback);

            return this;
        }

        public ActionRunnerParameters Handle<TException>()
            where TException : Exception
        {
            Handle<TException>(() => { });

            return this;
        }
    }
}
