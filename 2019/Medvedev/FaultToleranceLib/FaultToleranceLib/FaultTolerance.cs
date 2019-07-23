using System;

namespace FaultToleranceLib
{
    public class FaultTolerance
    {
        // TODO: Probably static methods?
        // TODO: Probably multiple exception support?
        private void DoAction(Type exceptionType, Action action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == exceptionType && i < count - 1)
                        continue;
                    else
                        throw ex;
                }
                break;
            }
        }

        public void Try(Type exceptionType, Action action, int count)
        {
            try
            {
                DoAction(exceptionType, action, count);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public void TryFallback(Type exceptionType, Action action, int count, Action fallback)
        {
            try
            {
                DoAction(exceptionType, action, count);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == exceptionType)
                    fallback();
                else
                    throw;
            }
        }
    }
}
