using Xunit;
using DataStructures;
using System;


namespace DataStructuresTests
{
    public class LinkedListTests
    {
        [Fact]
        public void AddLast_IncreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });
            int expected = list.Count + 1;

            list.AddLast(1);

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddFirst_IncreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });
            int expected = list.Count + 1;

            list.AddFirst(1);

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddAfter_IncreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });
            int expected = list.Count + 1;

            list.AddAfter(list.FindNode(2), 3);

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddBefore_IncreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });
            int expected = list.Count + 1;

            list.AddAfter(list.FindNode(4), 3);

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddAfter_AddAfterCurrentNode()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });
            string expected = "1234";

            list.AddAfter(list.FindNode(2), 3);

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddBefore_AddBeforeCurrentNode()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });
            string expected = "1234";

            list.AddBefore(list.FindNode(4), 3);

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddLast_AddAfterLastNode()
        {
            var list = new LinkedList<int>(new int[] { 1, 2, 3 });
            string expected = "1234";

            list.AddLast(4);

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddFirst_AddBeforeFirstNode()
        {
            var list = new LinkedList<int>(new int[] { 1, 2, 3 });
            string expected = "0123";

            list.AddFirst(0);

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddAfterForeignNode_ArgumentException()
        {
            LinkedList<int> list1 = new LinkedList<int>(new int[] { 1, 2, 3 });
            LinkedList<int> list2 = new LinkedList<int>(new int[] { 3, 4, 5 });

            Action action = () => list1.AddAfter(list2.FindNode(4), 6);

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void AddBeforeForeignNode_ArgumentException()
        {
            LinkedList<int> list1 = new LinkedList<int>(new int[] { 1, 2, 3 });
            LinkedList<int> list2 = new LinkedList<int>(new int[] { 3, 4, 5 });

            Action action = () => list1.AddBefore(list2.FindNode(4), 6);

            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void AddAfterNull_ArgumentNullException()
        {
            var list = new LinkedList<int>();

            Action action = () => list.AddAfter(null, 1);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void AddBeforeNull_ArgumentNullException()
        {
            var list = new LinkedList<int>();

            Action action = () => list.AddBefore(null, 1);

            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ContainsExistingItem_True()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });

            bool actual = list.Contains(2);

            Assert.True(actual);
        }

        [Fact]
        public void FindExistingValue_ActualNotNull()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });

            Node<int> actual = list.FindNode(2);

            Assert.NotNull(actual);
        }

        [Fact]
        public void ClearList_EmptyList()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });

            list.Clear();

            Assert.True(LinkedList<int>.IsNullOrEmpty(list));
        }

        [Fact]
        public void ClearList_CountEqualsToZero()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 4 });
            int expected = 0;

            list.Clear();
            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveFromEmptyList_InvalidOperationException()
        {
            LinkedList<int> list = new LinkedList<int>();

            Action action = () => list.Remove(1);

            Assert.Throws<InvalidOperationException>(action);
        }

        [Fact]
        public void Remove_DecreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });
            int expected = list.Count - 1;

            list.Remove(2);

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveLast_DecreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });
            int expected = list.Count - 1;

            list.RemoveLast();

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveFirst_DecreaseCount()
        {
            LinkedList<int> list = new LinkedList<int>(new int[] { 1, 2, 3, 4 });
            int expected = list.Count - 1;

            list.RemoveFirst();

            int actual = list.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Remove_RemovesFirstSuitableValue()
        {
            var list = new LinkedList<int>(new int[] { 1, 2, 3, 4, 3, 3 });
            string expected = "12433";

            list.Remove(3);

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveFirst_RemovesFirstValue()
        {
            var list = new LinkedList<int>(new int[] { 1, 2, 3 });
            string expected = "23";

            list.RemoveFirst();

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemoveLast_RemovesLastValue()
        {
            var list = new LinkedList<int>(new int[] { 1, 2, 3 });
            string expected = "12";

            list.RemoveLast();

            string actual = string.Join("", list);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsNullOrEmpty_ForEmptyList_True()
        {
            var list = new LinkedList<int>();
            Assert.True(LinkedList<int>.IsNullOrEmpty(list));
        }

        [Fact]
        public void IsNullOrEmpty_ForNull_True()
        {
            LinkedList<int> list = null;
            Assert.True(LinkedList<int>.IsNullOrEmpty(list));
        }
    }
}
