using Xunit;
using MyToDoList;

namespace ToDoListTestProject
{
    public class ToDoListTests
    {
        [Fact]
        public void ToDoListIsEmptyAfterInitializing()
        {
            Assert.Empty(new ToDoList());
        }

       [Fact]
        public void EditDescriptionSetsTheRightDescription()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};

            list.EditDescription(0, "Water the plants");

            Assert.Equal("Water the plants", list[0].Description);
        }

        [Fact]
        public void MarkAsCompleteChangesStateToTrue()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};

            list.MarkAsComplete(2);

            Assert.True(list[2].IsComplete);
        }
    }
}
