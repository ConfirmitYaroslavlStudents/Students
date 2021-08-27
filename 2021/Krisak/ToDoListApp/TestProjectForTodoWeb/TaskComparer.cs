using System;
using System.Collections;

namespace TestProjectForTodoWeb
{
    public class TaskComparer : IComparer
    {
        public int Compare(object? x, object? y)
        {
            return String.Compare(x.ToString(), y.ToString());
        }
    }
}
