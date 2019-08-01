using Xunit;
using Stack;
using System.Collections.Generic;
using System.Collections;

namespace StackTests
{
    public class StackTest
    {
        [Theory]
        [ClassData(typeof(StackClassData))]
        public void PushElement(IStack<int> stack)
        {
            stack.Push(1);

            Assert.Equal(1, stack.Pop());
        }

        [Theory]
        [ClassData(typeof(StackClassData))]
        public void PopEmptyStack(IStack<int> stack)
         {
            Assert.Throws<StackException>(() => stack.Pop());
         }

        [Theory]
        [ClassData(typeof(StackClassData))]
        public void Peek(IStack<int> stack)
         {
             stack.Push(5);

             Assert.Equal(5, stack.Peek());
             Assert.Equal(1, stack.Count);
         }

        [Theory]
        [ClassData(typeof(StackClassData))]
        public void PeekEmptyStack(IStack<int> stack)
         {
            Assert.Throws<StackException>(() => stack.Peek());
         }

        [Theory]
        [ClassData(typeof(StackClassData))]
        public void PushPop(IStack<int> stack)
         {
             stack.Push(1);

             Assert.Equal(1, stack.Pop());
             Assert.Equal(0, stack.Count);
         }

        [Theory]
        [ClassData(typeof(StackClassData))]
        public void PushTenElements(IStack<int> stack)
         {
             for (int i = 0; i < 10; i++)
             {
                 stack.Push(i);
             }

             Assert.Equal(10, stack.Count);
         }
    }

    public class StackClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new StackOnList<int>(){}
            };
            yield return new object[] {
                new StackOnArray<int>(){}
            };

        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
