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

        public static void Retry<Except>(Action method, int count, int timeout)
            where Except : Exception
        {
            if (count == 0)
            {
                return;
            }
            try
            {
                ActionTimeOut(method, timeout);
            }
            catch (Exception ex) when (ex is Except || ex is TimeoutException)
            {
                Retry<Except>(method, count - 1, timeout);
            }
        }

        public static Return Retry<Except, Return, Param>(Func<Param, Return> method, Param param, int count, int timeout)
            where Except : Exception
        {
            if (count == 0)
            {
                throw new FormatException("The value has not been obtained");
            }
            else
            {
                try
                {
                    return FunkTimeOut(method, param, timeout);
                }
                catch (Exception ex) when (ex is Except || ex is TimeoutException)
                {
                    return Retry<Except, Return, Param>(method, param, count - 1, timeout);
                }
            }
        }

        public static Return FallBack<Except, Return, Param>(Func<Param, Return> main, Func<Param, Return> spare, Param param, int timeout)
            where Except : Exception
        {
            try
            {
                return FunkTimeOut(main, param, timeout);
            }
            catch (Exception ex) when (ex is Except || ex is TimeoutException)
            {
                return spare(param);
            }
        }

        public static void FallBack<Except>(Action main, Action spare, int timeout)
            where Except : Exception
        {
            try
            {
                ActionTimeOut(main, timeout);
            }
            catch (Exception ex) when (ex is Except || ex is TimeoutException)
            {
                spare();
            }
        }

        private static void ActionTimeOut(Action action, int timeout)
        {
            var task = Task.Factory.StartNew(action);
            try
            {
                task.Wait(timeout);
                if (!task.IsCompleted)
                {
                    throw new TimeoutException();
                }
            }
            catch (AggregateException ex)
            {
                foreach (var except in ex.InnerExceptions)
                    throw except;
            }
        }

        private static Return FunkTimeOut<Param, Return>(Func<Param, Return> main, Param param, int timeout)
        {
            Task<Return> task = new Task<Return>(() => main(param));
            task.Start();
            try
            {
                task.Wait(timeout);
                if (!task.IsCompleted)
                    throw new TimeoutException();
                return task.Result;
            }
            catch (AggregateException ex)
            {
                foreach (var except in ex.InnerExceptions)
                    throw except;
            }
            return task.Result;
        }
    }
}
