using System;

namespace FaultToleranceLib
{
    public class FaultTolerance
    {
        public void Try(Action action, int count)
        {
            while (count > 0)
            {
                try
                {
                    action();
                }
                catch (Exception)
                {
                    if (count == 1)
                        throw;
                }
                count--;
            }
        }

        public void Fallback(Action action,Action fallBack)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                fallBack();
            }
        }

        public void Retry(Func<> action, int count)
        {

        }
    }
}
