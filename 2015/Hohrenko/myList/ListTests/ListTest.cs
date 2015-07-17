using NUnit.Framework;
using MyList;


namespace ListTests
{
    public class ListTest : TestIList
    {
        public override void CreateList()
        {
            sut = new List<int>();
        }
    }
    public class LinkedListTest : TestIList
    {
        public override void CreateList()
        {
            sut = new LinkedList<int>();
        }
    }

    public abstract class TestIList
    {
        protected IList<int> sut;
        [SetUp]
        public abstract void CreateList();
       
        [Test]
        public void AddTest()
        {
            Assert.That(sut, Is.Empty);
            sut.Add(5);

            Assert.That(sut.Count, Is.EqualTo(1));
        }

        [Test]
        public void Clear_countEqualZero()
        {
            sut.AddRange(new int[] { 5, 4, 3 });

            Assert.That(sut.Count, Is.GreaterThan(0));
            sut.Clear();
            Assert.That(sut.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCase(3, 2)]
        [TestCase(6, -1)]
        public void IndexOfTest(int val, int ind)
        {
            sut.AddRange(new int[] { 5, 4, 3 });

            Assert.That(sut.IndexOf(val), Is.EqualTo(ind));
        }

        [TestCase(5, true)]
        [TestCase(6, false)]
        public void ContainsTest(int val, bool b)
        {
            sut.Add(5);

            Assert.That(sut.Contains(val), Is.EqualTo(b));
        }

        [Test]
        public void RemoveAtTest()
        {
            sut.AddRange(new int[] { 5, 4, 3 });
            sut.RemoveAt(1);

            Assert.That(sut.Contains(4), Is.EqualTo(false));
        }

        [Test]
        public void InsertTest()
        {
            sut.AddRange(new int[] { 1, 2, 3, 4, 5 });
            sut.Insert(2, 11);        

            Assert.That(sut.IndexOf(11), Is.EqualTo(2));
            Assert.That(() => sut.Insert(15, 22), Throws.Exception);
            Assert.That(sut.Count, Is.EqualTo(6));
        }

    }
}
