using System;


namespace Cache
{
    public interface ITime<T>
    {
        TimeSpan TimeLive { get; }

        DateTime CurrenTime { get; }

        bool IsOldElement(Element<T> item);
    }
}
