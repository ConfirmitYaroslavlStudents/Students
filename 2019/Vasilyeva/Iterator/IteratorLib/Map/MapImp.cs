using System;

namespace IteratorLib
{
    public static class MapImp
    {
        public static IIterator<R> Map<T, R>(this IIterator<T> iterator, Func<T, R> func) => 
            new MapDecorator<T, R>(iterator, func);

    }
}
