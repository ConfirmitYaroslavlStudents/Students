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
                catch (Exception ex)
                {
                    if (count == 1)
                        throw ex;
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
            catch (Exception ex)
            {
                fallBack();
                Console.WriteLine(ex.GetType() + " Plan B in development");
            }
        }
    }
}
