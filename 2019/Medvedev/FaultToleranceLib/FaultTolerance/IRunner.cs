using System;

namespace FaultTolerance
{
    public interface IRunner
    {
        bool Run(Action action);
    }
}