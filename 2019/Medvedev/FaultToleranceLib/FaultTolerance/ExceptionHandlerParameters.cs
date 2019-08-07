using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    internal class ExceptionHandlerParameters
    {
        private int _timeout;
        internal Dictionary<Type, Action> Fallbacks { get; }

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

        internal ExceptionHandlerParameters(ExceptionHandlerParameters p)
        {
            Timeout = p._timeout;
            Fallbacks = new Dictionary<Type, Action>(p.Fallbacks);
        }

        internal ExceptionHandlerParameters()
        {
            Fallbacks = new Dictionary<Type, Action>();
            Timeout = -1;
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