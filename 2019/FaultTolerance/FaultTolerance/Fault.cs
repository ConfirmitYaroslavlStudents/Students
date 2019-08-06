using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public class Fault
    {
        private Dictionary<Type, int> _knownExceptions = new Dictionary<Type, int>();

        public Fault Retry<T>(int count)
        {
            var type = typeof(T);

            if (_knownExceptions.ContainsKey(type))
            {
                _knownExceptions[type] = count;
            }
            else
            {
                _knownExceptions.Add(type, count);
            }

            return this;
        }

        public void Launch(Action action)
        {
            while (!TryAction(action))
            {

            }
        }

        private bool TryAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                var type = e.GetType();

                if (!_knownExceptions.ContainsKey(type) || _knownExceptions[e.GetType()] == 0)
                {
                    throw;
                }

                _knownExceptions[type]--;

                return false;
            }

            return true;
        }
    }
}
