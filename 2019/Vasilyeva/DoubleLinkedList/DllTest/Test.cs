using System;
using Xunit;
using DllLIb;

namespace DllTest
{
    public class Test
    {
        [Fact]
        public void InitStracrture()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();

            Assert.Equal(0, dll.Count);
        }
        [Theory]
        [InlineData(new[] { 3, 2, 1 })]
        [InlineData(new[] { 3 })]
        public void AddLast_Equal_SimpleArray(int[] array)
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();

            foreach (var value in array)
            {
                dll.AddLast(value);
            }

            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(array[i], item);
                i++;
            }

            Assert.Equal(array.Length, dll.Count);
        }
        [Theory]
        [InlineData(new[] { 3, 2, 1 })]
        [InlineData(new[] { 3, 2 })]
        public void AddFirst_Equal_SimpleArray(int[] array)
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();

            foreach (var value in array)
            {
                dll.AddFirst(value);
            }

            int i = array.Length - 1;
            foreach (var item in dll)
            {
                Assert.Equal(array[i], item);
                i--;
            }

            Assert.Equal(array.Length, dll.Count);
        }
        [Theory]
        [InlineData(new[] { 3, 2, 1 }, 0, 4, new[] { 3, 2, 1, 0 })]
        [InlineData(new[] { 3, 2, 1 }, 0, 1, new[] { 3, 0, 2, 1 })]
        [InlineData(new int[0], 1, 0, new[] { 1 })]
        public void InsertNewItemIn_Equal_NewDll(int[] array, int value, int index, int[] expected)
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            if (array != null)
                foreach (var val in array)
                {
                    dll.AddLast(val);
                }

            dll.Insert(index, value);

            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(expected[i], item);
                i++;
            }

            Assert.Equal(array.Length + 1, dll.Count);
        }

        [Fact]
        public void DeleteLastInDll_Equal_Simple()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddLast(3);

            dll.DeleteLast();

            int[] expected = new int[] { 1, 2 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(expected[i], item);
                i++;
            }
        }
        [Fact]
        public void DeleteFirstInDll_Equal_Simple()
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            dll.AddLast(1);
            dll.AddLast(2);
            dll.AddLast(3);

            dll.DeleteFirst();

            int[] expected = new int[] { 2, 3 };
            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(expected[i], item);
                i++;
            }
        }
        [Theory]
        [InlineData(new[] { 3, 2, 1 }, 2, new[] { 3, 2 })]
        [InlineData(new[] { 3, 2, 1 }, 1, new[] { 3, 1 })]
        [InlineData(new[] { 3, 2, 1 }, 0, new[] { 2, 1 })]
        public void RemoveAtElement_Equal_NewDll(int[] array, int index, int[] expected)
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            if (array != null)
                foreach (var val in array)
                {
                    dll.AddLast(val);
                }

            dll.RemoveAt(index);

            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(expected[i], item);
                i++;
            }

            Assert.Equal(expected.Length, dll.Count);
        }
        [Theory]
        [InlineData(new[] { 3, 2, 1 }, 2, new[] { 3, 1 })]
        [InlineData(new[] { 3, 2, 1 }, 1, new[] { 3, 2 })]
        [InlineData(new[] { 3, 2, 1 }, 0, new[] { 3, 2, 1 })]
        public void RemoveElement_Equal_NewDll(int[] array, int value, int[] expected)
        {
            DoubleLinkedList<int> dll = new DoubleLinkedList<int>();
            if (array != null)
                foreach (var val in array)
                {
                    dll.AddLast(val);
                }

            dll.Remove(value);

            int i = 0;
            foreach (var item in dll)
            {
                Assert.Equal(expected[i], item);
                i++;
            }

            Assert.Equal(expected.Length, dll.Count);
        }
    }
}
