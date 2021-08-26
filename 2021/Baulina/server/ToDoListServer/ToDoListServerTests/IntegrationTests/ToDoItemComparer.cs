using System.Collections.Generic;
using System.Linq;
using ToDoApi.Models;

namespace ToDoListServerTests.IntegrationTests
{
    public class ToDoItemComparer : IEqualityComparer<ToDoItem>
    {
        public bool Equals(ToDoItem x, ToDoItem y)
        {
            return y != null && x != null && x.Status == y.Status && x.Description.Equals(y.Description) &&
                   x.Id == y.Id && CompareTags(x.Tags, y.Tags);
        }

        public int GetHashCode(ToDoItem obj)
        {
            return obj.Id.GetHashCode();
        }

        private bool CompareTags(IEnumerable<Tag> x, IEnumerable<Tag> y)
        {
            if (x == null && y == null) return true;
            return x != null && x.SequenceEqual(y);
        }
    }
}
