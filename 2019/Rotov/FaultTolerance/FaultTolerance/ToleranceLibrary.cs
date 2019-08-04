using System;
using System.Threading.Tasks;

namespace FaultTolerance
{
    //TODO no need in spare function for retry
    //TODO support multiple exceptions per case
    //TODO support combinations of retry and fallback
    //TODO timeout method OK
    public static class ToleranceLibrary
    {

        public static void Retry<Except>(Action method , int count , int ms)
            where Except : Exception
        {
            if(count == 0)
            {
                return;
            }
            try
            {
                var task = Task.Factory.StartNew(method);
                try
                {
                    task.Wait(ms);
                    if(!task.IsCompleted)
                        throw new TimeoutException("Something went wrong");
                }
                catch(AggregateException ex)
                {
                    throw ex.InnerException;
                }
            }
            catch(Exception ex) when(ex is Except || ex is TimeoutException)
            {
                Retry<Except>(method , count - 1 , ms);
            }
        }

        public static Return Retry<Except, Return, Param>(Func<Param , Return> method , Param param , int count)
            where Except : Exception
        {
            if(count == 1)
            {
                throw new FormatException("The value has not been obtained");
            }
            else
            {
                try
                {
                    return method(param);
                }
                catch(Except)
                {
                    return Retry<Except , Return , Param>(method , param , count - 1);
                }
            }
        }

        public static Return FallBack<Except, Return, Param>(Func<Param , Return> main , Func<Param , Return> spare , Param param , int ms)
            where Except : Exception
        {
            try
            {
                try
                {
                    Task<Return> task = new Task<Return>(() => main(param));
                    task.Start();
                    task.Wait(ms);
                    if(!task.IsCompleted)
                        throw new TimeoutException();
                    return task.Result;
                }
                catch(AggregateException ex)
                {
                    throw ex.InnerException;
                }
            }
            catch(Exception ex) when(ex is Except || ex is TimeoutException)
            {
                return spare(param);
            }
        }

        public static void FallBack<Except>(Action main , Action spare , int timeout)
            where Except : Exception
        {
            try
            {
                var task = Task.Factory.StartNew(main);
                try
                {
                    task.Wait(timeout);
                    if(!task.IsCompleted)
                    {
                        throw new TimeoutException("Something went wrong");
                    }
                }
                catch(AggregateException ex)
                {
                    throw ex.InnerException;
                }
            }
            catch(Exception ex) when(ex is Except || ex is TimeoutException)
            {
                spare();
            }
        }
    }
}
