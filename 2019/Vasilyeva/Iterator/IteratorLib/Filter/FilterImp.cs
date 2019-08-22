using System;

namespace IteratorLib
{
    public static class FilterImp
    {
        public static IIterator<T> Filter<T>(this IIterator<T> iterator, Predicate<T> condition) =>
            new FilterDecorator<T>(iterator, condition);
    }
}
