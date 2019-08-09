using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    internal class ExceptionHandlerParameters
    {
        private int _timeout;
        private int _countOfRepeats;
        internal Dictionary<Type, Action> Fallbacks { get; }

        internal int CountOfRepeats
        {
            get => _countOfRepeats;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Count of repeats must be positive");

                _countOfRepeats = value;
            }
        }

        internal int Timeout
        {
            get => _timeout;
            set
            {
                if (value <= 0 && value != -1)
                    throw new ArgumentException("Timeout must be positive ot -1 for infinity");

                _timeout = value;
            }
        }

        internal ExceptionHandlerParameters()
        {
            Fallbacks = new Dictionary<Type, Action>();
            Timeout = -1;
            CountOfRepeats = 1;
        }

        internal void Handle<TException>(Action fallback)
            where TException : Exception
        {
            if (Fallbacks.ContainsKey(typeof(TException)))
                throw new ArgumentException("Fallback for such case is already exists");

            Fallbacks.Add(typeof(TException), fallback);
        }
    }
}