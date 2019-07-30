using System;

namespace FaultTolerance
{
    public class Runner : IRunner
    {
        public bool Run(Action action)
        {
            action();

            return true;
        }
    }
}