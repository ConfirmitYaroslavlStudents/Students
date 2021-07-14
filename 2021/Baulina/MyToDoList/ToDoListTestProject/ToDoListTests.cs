using System;
using Xunit;
using MyToDoList;

namespace ToDoListTestProject
{
    public class ToDoListTests
    {
        [Fact]
        public void ToDoListIsEmptyAfterInitializing()
        {
            var list = new ToDoList();

            Assert.True(list.IsEmpty);
        }

        [Fact]
        public void IndexerGetsRightValueWhenIndexIsCorrect()
        {
            var list = new ToDoList();
            var expected = new ToDoItem("Do the laundry", false);

            list.Add("Clean the house");
            list.Add("Do the laundry");
            list.Add("Iron the clothes");


            Assert.Equal(expected, list[1]);
        }

        [Fact]
        public void IndexerThrowsExceptionWithIncorrectIndex()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};



            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                list.Add(list[-1]);
                list.Add(list[4]);
            });
        }

        [Fact]
        public void IndexerSetsRight()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};

            list[0] = new ToDoItem("Water the plants");

            Assert.Equal("Water the plants", list[0].Description);
        }

        [Fact]
        public void AddIncreasesListCount()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};


            Assert.Equal(3, list.Count);
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

        [Fact]
        public void RemoveDecreasesListCount()
        {
            var list = new ToDoList
            {
                "Clean the house",
                "Do the laundry",
                "Iron the clothes"
            };

            list.Remove(2);

            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void ToArrayProcessesAllData()
        {
            var list = new ToDoList {"Clean the house", "Do the laundry", "Iron the clothes"};

            var result = list.ToArray();

            Assert.Equal(list, result);
        }
    }
}
