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
            var list = new ToDoList(new[]
            {
                new ToDoItem {Description = "Clean the house"},
                new() {Description = "Water the plants"}
            });

            list.AddToDoItem(new() {Description = "Iron the clothes"});

            Assert.Equal(2, list[2].Id);
        }
    }
}
