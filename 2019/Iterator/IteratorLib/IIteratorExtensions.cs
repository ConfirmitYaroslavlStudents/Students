using System;

namespace IteratorLib
{
    public static class IIteratorExtensions
    {
        public static IIterator<R> Map<T, R>(this IIterator<T> iterator, Func<T, R> map)
        {
            return new MapDecoratedIterator<T, R>(iterator, map);
        }

        public static IIterator<T> Filter<T>(this IIterator<T> iterator, Func<T, bool> filter)
        {
            return new FilterDecoratedIterator<T>(iterator, filter);
        }
    }
}