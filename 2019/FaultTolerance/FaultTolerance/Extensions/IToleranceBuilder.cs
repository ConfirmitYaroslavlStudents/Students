using System;

namespace FaultTolerance.Extensions
{
    public interface IToleranceBuilder
    {
        void Execute(Action action);
    }
}