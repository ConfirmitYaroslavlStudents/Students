using System;
using System.Collections.Generic;
using Xunit;

namespace TreeLCRS.Tests
{
    public class TreeTests
    {
        [Fact]
        public void SetRoot_EmptyTree_ShouldWork()
        {
            //Arrange
            Tree<int> tree = new Tree<int>();
            //Act
            tree.SetRoot(8);
            //Assert
            Assert.True(tree.Contains(8));
        }

        [Fact]
        public void SetRoot_NonEmptyTree_ShouldFail()
        {
            Tree<int> tree = new Tree<int>(10);

            Assert.Throws<InvalidOperationException>(() => tree.SetRoot(5));
        }

        [Theory]
        [InlineData(10, 10, "data")]
        [InlineData(3, 5, "parentData")]
        public void Insert_InvalidData_ShouldFail(int nodeData, int parentData, string param)
        {
            Tree<int> tree = new Tree<int>(10);

            Assert.Throws<ArgumentException>(param, () => tree.Insert(nodeData, parentData));
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void Insert_EmptyTree_ShouldFail()
        {
            Tree<int> tree = new Tree<int>();
            Assert.Throws<InvalidOperationException>(() => tree.Insert(5, 7));
            Assert.True(tree.IsEmpty());
        }

        [Fact]
        public void Count_SuccessfulInsert_ShouldIncrease()
        {
            Tree<int> tree = new Tree<int>(10);
            tree.Insert(5, 10);

            int expected = 2;
            Assert.Equal(expected, tree.Count);
        }

        [Fact]
        public void Traverse_ToList_ShouldWork()
        {
            Tree<int> tree = new Tree<int>(5);
            tree.Insert(4, 5);
            tree.Insert(3, 4);
            tree.Insert(2, 4);
            tree.Insert(1, 5);
            List<int> expected = new List<int>() { 5, 4, 3, 2, 1 };

            List<int> actual = new List<int>();
            tree.Traverse(actual.Add);

            Assert.Equal(expected, actual);
        }
    }
}
