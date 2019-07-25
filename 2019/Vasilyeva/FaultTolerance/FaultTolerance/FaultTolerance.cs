using System;

namespace FaultTolerance
{
    public static class FaultTolerance
    {
        #region action с параметрами
        public static void Retry<TException, TParameter>(Action<TParameter> method, int count, TParameter parameter) where TException : Exception
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    method(parameter);
                }
                catch (TException)
                {
                    if (i + 1 == count)
                        throw;
                    continue;
                }
                catch
                {
                    throw;
                }
            }
        }

        public static void FallBack<TException, TParameter>(Action<TParameter> firstMethod, Action<TParameter> secondMethod, TParameter parameter) where TException : Exception
        {
            try
            {
                firstMethod(parameter);
            }
            catch (TException)
            {
                secondMethod(parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region func с параметрами
        public static TResult Retry<TException,TParameter, TResult>(Func<TParameter,TResult> func, TParameter parameter, int count) where TException : Exception
        {
            for (int i = 0; i < count; i++)
            {
                try
                {
                    return func(parameter);
                }
                catch (TException)
                {
                    if (i + 1 == count)
                        throw;
                    continue;
                }
                catch
                {
                    throw;
                }
            }
            return default;
        }

        public static TResult FallBack<TException, TParameter, TResult>(Func<TParameter,TResult> firstMethod, Func<TParameter,TResult> secondMethod, TParameter parameter) where TException : Exception
        {
            try
            {
                return firstMethod(parameter);
            }
            catch (TException)
            {
                return secondMethod(parameter);
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region action без параметров
        public static void Retry<TException> (Action method, int count) where TException : Exception
        {
            void action(int num) => method();
            Retry<TException, int>(action, count, 0);
        }

        public static void FallBack<TException>(Action firstMethod, Action secondMethod) where TException : Exception
        {
            Action<int> actionF = (int num) => firstMethod();
            Action<int> actionS = (int num) => secondMethod();
            FallBack<TException, int>(actionF, actionS, 0);

        }
        #endregion

        #region func без параметров
        public static TResult Retry<TException, TResult>(Func<TResult> func, int count) where TException : Exception
        {
            Func<int, TResult> altfunc = (int num) => func();
            return Retry<TException, int, TResult>(altfunc, 0, count);
        }

        public static TResult FallBack<TException, TResult>(Func<TResult> firstMethod, Func<TResult> secondMethod) where TException : Exception
        {
            Func<int, TResult> altfuncF = (int num) => firstMethod();
            Func<int, TResult> altfuncS = (int num) => secondMethod();
            return FallBack<TException, int, TResult>(altfuncF, altfuncS, 0);

        }
        #endregion
    }
}
