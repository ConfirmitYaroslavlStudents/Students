using System;
using System.Collections.Generic;

namespace FaultTolerance
{
    public static class ExceptionHandler
    {
        private static Dictionary<Type, Action> otherWaysDictionary;
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
        
        public static void Fallback(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                otherWaysDictionary[e.GetType()].Invoke();
            }
            
        }

        public static void AddOtherWay<TException>(Action action) where TException: Exception
        {
            otherWaysDictionary[typeof(TException)] = action;
        }

        public static void DeleteOtherWay<TException>() where TException : Exception
        {
            otherWaysDictionary.Remove(typeof(TException));
        }

    }
    
}
