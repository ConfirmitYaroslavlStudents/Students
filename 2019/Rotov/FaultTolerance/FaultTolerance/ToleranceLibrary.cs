using System;

namespace FaultTolerance
{
    public static class ToleranceLibrary
    {

        public static void Retry<T>(Action method, Action spare, int count) where T: Exception
        {
            if (count == 0)
            {
                FallBack<T>(method, spare);
                return;
            }
            try
            {
                method();
            }
            catch(T)
            {
                Retry<T>(method, spare,  count - 1);
            }
        }


        public static Return Retry<Exception, Return>(Func<Return> method, Func<Return> spare, int count) where Exception : System.Exception
        {
            if (count == 0)
            {
                return FallBack<Exception, Return>(method, spare);
            }
            try
            {
                return method();
            }
            catch (Exception)
            {
                Retry<Exception, Return>(method, spare, count - 1);
            }
            return method();
        }


        public static R FallBack<E, R>(Func<R> main, Func<R> spare) where E : Exception
        {
            try
            {
                return main();
            }
            catch (E)
            {
                try
                {
                    return spare();
                }
                catch (E)
                {
                    throw;
                }
            }
        }


        public static void FallBack<E>(Action main, Action spare) where E: Exception
        {
            try
            {
                main();
            }
            catch (E)
            {
                try
                {
                    spare();
                }
                catch (E)
                {
                    throw;
                }
            }
        }
    }
}
