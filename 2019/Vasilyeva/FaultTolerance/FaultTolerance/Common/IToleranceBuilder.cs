using System;

namespace FaultTolerance
{
    public interface IToleranceBuilder
    {
        void Execute(Action action);
    }
}
