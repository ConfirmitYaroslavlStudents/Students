using System;

namespace FaultTolerance
{
    public class ExceptionHandler
    {
        private ExceptionHandlerParameters _parameters;

        private ExceptionHandler(ExceptionHandler e)
        {
            _parameters = new ExceptionHandlerParameters(e._parameters);
        }

        public ExceptionHandler()
        {
            _parameters = new ExceptionHandlerParameters();
        }

        public ExceptionHandler Handle<TException>(Action fallback) 
            where TException : Exception
        {
            var handler = new ExceptionHandler(this);
            handler._parameters.Handle<TException>(fallback);

            return handler;
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
            var handler = new ExceptionHandler(this);
            handler._parameters.Timeout = timeout;

            return handler;
        }

        public ExceptionHandler Run(Action action, int count)
        {
            try
            {
                DoAction(action, count);
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
            var runner = new TimeoutRunner(_parameters.Timeout);
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
                    throw ;
                }

                break;
            }
        }
    }
}