using ToDoApi.Models;
using Xunit;

namespace ToDoListServerTests
{
    public class ToDoListTests
    {
        [Fact]
        public void ToDoListIsEmptyAfterInitializing()
        {
            Assert.Empty(new ToDoList());
        }

        [Fact]
        public void ToDoItemIdIsSetCorrectAfterAddition()
        {
            var list = new ToDoList { "Clean the house", "Do the laundry" };
            
            list.Add("Iron the clothes");

            Assert.Equal(2, list[2].Id);
        }
    }
}
