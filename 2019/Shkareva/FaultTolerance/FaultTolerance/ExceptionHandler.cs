using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public static class ExceptionHandler
    {
        public static void Retry<TException>(int effortCount, Action action) where TException: Exception
        {
            try
            {
                effortCount--;
                action.Invoke();                
            }
            catch(TException)
            {
                if (effortCount == 0)
                {
                    throw (TException)Activator.CreateInstance(typeof(TException));
                }
                Retry<TException>(effortCount, action);
            }
        }
        
        public static void Fallback<TException>(Action action, Action secondAction) where TException : Exception
        {
            try
            {
                action.Invoke();
            }
            catch (TException)
            {
                secondAction.Invoke();
            }
            
        }

    }
    
}
