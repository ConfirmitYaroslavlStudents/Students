using System;
using System.Threading.Tasks;

namespace FaultTolerance
{
    public class ExceptionHandler
    {
        private ExceptionHandlerParameters _parameters;

        public ExceptionHandler()
        {
            _parameters = new ExceptionHandlerParameters();
        }

        public ExceptionHandler Handle<TException>(Action fallback) 
            where TException : Exception
        {
            _parameters.Handle<TException>(fallback);

            return this;
        }

        public ExceptionHandler Handle<TException>()
            where TException : Exception
        {
            return Handle<TException>(() => { });
        }

        public ExceptionHandler HandleFailedRun(Action fallback)
        {
            return Handle<RunFailedException>(fallback);
        }

        public ExceptionHandler WithTimeout(int timeout)
        {
            _parameters.Timeout = timeout;

            return this;
        }

        public ExceptionHandler Repeat(int count)
        {
            _parameters.CountOfRepeats = count;

            return this;
        }

        public ExceptionHandler Run(Action action)
        {
            try
            {
                DoAction(action, _parameters.CountOfRepeats);
            }
            catch (Exception ex)
            {
                if (_parameters.Fallbacks.ContainsKey(ex.GetType()))
                    _parameters.Fallbacks[ex.GetType()].Invoke();
                else
                    throw;
            }

            return this;
        }

        private void DoAction(Action action, int count)
        {
            IRunner runner = null;
            if (_parameters.Timeout != -1)
                runner = new TimeoutRunner(_parameters.Timeout);
            else
                runner = new Runner();

            for (int i = 0; i < count; i++)
            {
                try
                {
                    var succeed = runner.Run(action);
                    if (!succeed)
                        throw new RunFailedException();
                }
                catch (Exception ex)
                {
                    if (_parameters.Fallbacks.ContainsKey(ex.GetType()) && i < count - 1)
                        continue;
                    throw;
                }

                break;
            }
        }
    }
}