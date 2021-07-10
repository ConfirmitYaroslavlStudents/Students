using System;
using LinkedListBaulina;
using Xunit;
using msLinkedListInt = System.Collections.Generic.LinkedList<int>;
using msLinkedListString = System.Collections.Generic.LinkedList<string>;
using msLinkedListBook = System.Collections.Generic.LinkedList<LinkedListTestProject.Book>;

namespace LinkedListTestProject
{
    public class LinkedListTests
    {
       [Fact]
        public void IsEmptyIsTrueForEmptyLinkedList()
        {
            var list = new LinkedList<int>();

            Assert.True(list.IsEmpty);
        }

        [Fact]
        public void RemoveRemovesTheFirstOccurrenceOfTheSpecifiedIntValue()
        {
            var list = new LinkedList<int>(new[] {1, 2, 3, 2, 2});
            var expectedResult = new msLinkedListInt(new[] {1, 3, 2, 2});

            list.Remove(2);

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void RemoveRemovesTheFirstOccurrenceOfTheSpecifiedStringValue()
        {
            var list = new LinkedList<string>(new[] {"1", "2", "3", "2", "2"});
            var expectedResult = new msLinkedListString(new[] {"1", "3", "2", "2"});

            list.Remove("2");

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void RemoveRemovesTheFirstOccurrenceOfTheSpecifiedBookValue()
        {
            var inputCollection = new []
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            };
            var list = new LinkedList<Book>(inputCollection);
            var expectedResult = new msLinkedListBook(new[]
            {
                new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            });

            list.Remove(inputCollection[0]);

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void RemoveIntValueDecreasesCount()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedCount = 4;

            list.Remove(2);

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void RemoveStringValueDecreasesCount()
        {
            var list = new LinkedList<string>(new[] {"0", "1", "2", "3", "4"});
            var expectedCount = 4;

            list.Remove("2");

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void RemoveBookValueDecreasesCount()
        {
            var inputCollection = new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            };
            var list = new LinkedList<Book>(inputCollection);
            var expectedCount = 2;

            list.Remove(inputCollection[0]);

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void RemoveFromEmptyListThrowsException()
        {
            var list = new LinkedList<int>();

            Assert.Throws<ArgumentException>(() => { list.Remove(5); });
        }

        [Fact]
        public void CountIsZeroAfterClear()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedCount = 0;

            list.Clear();

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void ContainsExistingIntValueReturnsTrue()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var sampleDataFromList = 1;

            Assert.True(list.Contains(sampleDataFromList));
        }

        [Fact]
        public void ContainsExistingStringValueReturnsTrue()
        {
            var list = new LinkedList<string>(new[] {"0", "1", "2", "3", "4"});
            var sampleDataFromList = "1";

            Assert.True(list.Contains(sampleDataFromList));
        }

        [Fact]
        public void ContainsExistingBookValueReturnsTrue()
        {
            var inputCollection = new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            };
            var list = new LinkedList<Book>(inputCollection);
            var sampleDataFromList = new Book("Ash", "M. Lo", 5.75);

            Assert.True(list.Contains(sampleDataFromList));
        }

        [Fact]
        public void ContainsNonExistingDataReturnsFalse()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var sampleDataNotFromList = 6;

            Assert.False(list.Contains(sampleDataNotFromList));
        }

        [Fact]
        public void AddInsertsIntToTheEndOfList()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedResult = new msLinkedListInt(new[] {0, 1, 2, 3, 4, 5});

            list.Add(5);

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void AddInsertsStringToTheEndOfList()
        {
            var list = new LinkedList<string>(new[] {"0", "1", "2", "3", "4"});
            var expectedResult = new msLinkedListString(new[] {"0", "1", "2", "3", "4", "5"});

            list.Add("5");

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void AddInsertsBookToTheEndOfList()
        {
            var inputCollection = new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            };
            var list = new LinkedList<Book>(inputCollection);
            var expectedResult = new msLinkedListBook(new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00), new Book("1984", "G. Orwell", 7.83)
            });

            list.Add(new Book("1984", "G. Orwell", 7.83));

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void AddIncreasesCount()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedCount = 6;

            list.Add(5);

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void AppendFirstIncreasesCount()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedCount = 6;

            list.AppendFirst(5);

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void AppendFirstForStringKeepsRemainingElementsInOrder()
        {
            var list = new LinkedList<string>(new[] {"0", "1", "2", "3", "4"});
            var expectedResult = new msLinkedListString(new[] {"5", "0", "1", "2", "3", "4"});

            list.AppendFirst("5");

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void AppendFirstForIntKeepsRemainingElementsInOrder()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedResult = new msLinkedListInt(new[] {5, 0, 1, 2, 3, 4});

            list.AppendFirst(5);

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void InsertIncreasesCount()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedCount = 6;
            var indexToInsertAt = 3;

            list.Insert(indexToInsertAt, 6);

            Assert.Equal(expectedCount, list.Count);
        }

        [Fact]
        public void InsertIntDoesNotChangeOrderOfElementsBeforeAndAfterIndex()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var expectedResult = new msLinkedListInt(new[] {0, 1, 2, 6, 3, 4});
            var indexToInsertAt = 3;

            list.Insert(indexToInsertAt, 6);

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void InsertStringDoesNotChangeOrderOfElementsBeforeAndAfterIndex()
        {
            var list = new LinkedList<string>(new[] {"0", "1", "2", "3", "4"});
            var expectedResult = new msLinkedListString(new[] {"0", "1", "2", "6", "3", "4"});
            var indexToInsertAt = 3;

            list.Insert(indexToInsertAt, "6");

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void InsertBookDoesNotChangeOrderOfElementsBeforeAndAfterIndex()
        {
            var inputCollection = new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("Annabel", "K. Winter", 9.00)
            };
            var list = new LinkedList<Book>(inputCollection);
            var expectedResult = new msLinkedListBook(new[]
            {
                new Book("Annabel", "K. Winter", 9.00), new Book("Ash", "M. Lo", 5.75),
                new Book("1984", "G. Orwell", 7.83),
                new Book("Annabel", "K. Winter", 9.00)
            });
            var indexToInsertAt = 2;

            list.Insert(indexToInsertAt, new Book("1984", "G. Orwell", 7.83));

            Assert.Equal(expectedResult, list);
        }

        [Fact]
        public void InsertAtNegativeIndexThrowsException()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var indexToInsertAt = -3;

            Assert.Throws<IndexOutOfRangeException>(() => { list.Insert(indexToInsertAt, 6); });

        }

        [Fact]
        public void InsertAtIndexGreaterThanCountThrowsException()
        {
            var list = new LinkedList<int>(new[] {0, 1, 2, 3, 4});
            var indexToInsertAt = list.Count + 1;

            Assert.Throws<IndexOutOfRangeException>(() => { list.Insert(indexToInsertAt, 6); });
        }

        [Fact]
        public void InsertAtZeroIndexInEmptyListDoesNotThrowException()
        {
            var list = new LinkedList<int>();
            var indexToInsertAt = list.Count;

            list.Insert(indexToInsertAt, 6);
        }
    }

    class Book : IEquatable<Book>
    {
        public Book(string title, string author, double price)
        {
            Title = title;
            Author = author;
            Price = price;
        }
        private string Title { get; }
        private string Author { get; }
        private double Price { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Book);
        }
        public bool Equals(Book other)
        {
            return other != null && Title.Equals(other.Title) && Author.Equals(other.Author) && (Price - other.Price) < double.Epsilon;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Author, Price);
        }
    }
}
