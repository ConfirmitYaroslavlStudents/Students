using System;

namespace FaultTolerance
{
    //TODO no need in spare function for retry
    //TODO support multiple exceptions per case
    //TODO support combinations of retry and fallback
    //TODO timeout method
    public static class ToleranceLibrary
    {

        public static void Retry<Except>(Action method, Action spare, int count)
            where Except : Exception
        {
            if (count == 1)
            {
                FallBack<Except>(method, spare);
                return;
            }
            try
            {
                method();
            }
            catch (Except)
            {
                Retry<Except>(method, spare, count - 1);
            }
        }

        public static Return Retry<Except, Return, Param>(Func<Param, Return> method, Func<Param, Return> spare, Param param , int count)
            where Except : Exception
        {
            if (count == 1)
            {
                return FallBack<Except, Return, Param>(method, spare, param);
            }
            else
            {
                try
                {
                    return method(param);
                }
                catch (Except)
                {
                    return Retry<Except, Return, Param>(method, spare, param, count - 1);
                }
            }
        }

        public static Return FallBack<Except, Return, Param>(Func<Param, Return> main, Func<Param, Return> spare, Param param)
            where Except : Exception
        {
            try
            {
                return main(param);
            }
            catch (Except)
            {
                try
                {
                    return spare(param);
                }
                catch (Except)
                {
                    throw;
                }
            }
        }

        public static void FallBack<Except>(Action main, Action spare)
            where Except : Exception
        {
            try
            {
                main();
            }
            catch (Except)
            {
                try
                {
                    spare();
                }
                catch (Except)
                {
                    throw;
                }
            }
        }
    }
}
