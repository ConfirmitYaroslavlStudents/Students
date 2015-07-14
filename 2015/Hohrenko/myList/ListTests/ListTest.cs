
using myList;
using NUnit.Framework;

namespace ListTests
{
    [TestFixture]
    public class ListTest
    {
        [Test]
        public void AddTest()
        {
            var sut = new MyList<int>();

            Assert.That(sut, Is.Empty);

            sut.Add(5);

            Assert.That(sut[0], Is.EqualTo(5));
            Assert.That(sut.Count, Is.EqualTo(1));
        }

        [Test]
        public void Clear_countEqualZero()
        {
            var sut = new MyList<int> {5, 3, 4};

            Assert.That(sut.Count, Is.GreaterThan(0));
            sut.Clear();
            Assert.That(sut.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCase(3, 1)]
        [TestCase(6, -1)]
        public void IndexOfTest(int val, int ind)
        {
            var sut = new MyList<int> {5, 3, 4};

            Assert.That(sut.IndexOf(val), Is.EqualTo(ind));
        }

        [Test]
        public void IndexOfObjectTest()
        {
            var sut = new MyList<MyList<int>>();
            var l1 = new MyList<int>();
            var l2 = new MyList<int>();

            sut.Add(l1);

            Assert.That(sut.IndexOf(l2), Is.EqualTo(-1));
            Assert.That(sut.IndexOf(l1), Is.EqualTo(0));
        }
        

        [Test]
        [TestCase(5, true)]
        [TestCase(6, false)]
        public void ContainsTest(int val, bool b)
        {
            var sut = new MyList<int> {5};

            Assert.That(sut.Contains(val), Is.EqualTo(b));
        }

        [Test]
        public void RemoveAtTest()
        {
            var sut = new MyList<int> { 5, 3, 4 };
            sut.RemoveAt(2);

            Assert.That(sut.Contains(4), Is.EqualTo(false));
        }

        [Test]
        public void InsertTest()
        {
            var sut = new MyList<int> {1, 2, 3, 4, 5};
            sut.Insert(2, 11);
            sut.Insert(15, 22);

            Assert.That(sut.IndexOf(11), Is.EqualTo(2));
            Assert.That(sut.Count, Is.EqualTo(6));
        }

    }
}
