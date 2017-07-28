using System;
using System.Collections.Generic;

namespace FizzBuzz
{
    public class DescendingComparer<T> : IComparer<T> where T : IComparable<T>
    {
        public int Compare(T first, T second)
        {
            return second.CompareTo(first);
        }
    }
}