using System.Collections.Generic;
using ToDoApi.Models;

namespace ToDoListServerTests.IntegrationTests
{
    public class ToDoItemComparer: IEqualityComparer<ToDoItem>
    {
        public bool Equals(ToDoItem x, ToDoItem y)
        {
            return y != null && x != null && x.Status == y.Status && x.Description.Equals(y.Description) &&
                   x.Id == y.Id;
        }

        public int GetHashCode(ToDoItem obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
