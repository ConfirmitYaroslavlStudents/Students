using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetLib;

namespace SetTests
{
    [TestClass]
    public class SetTests
    {
        private readonly int[] _defaultCollection;

        public SetTests()
        {
            _defaultCollection = new[] { 1, 2, 3, 4, 5, 6 };
        }

        [TestMethod]
        public void DefaultConstructor()
        {
            Set<int> intSet = new Set<int>();

            Assert.AreEqual(0, intSet.Count);
        }

        [TestMethod]
        public void ConstructorWithCollection()
        {
            Set<int> intSet = new Set<int>(_defaultCollection);

            Assert.AreEqual(6, intSet.Count);
        }

        [TestMethod]
        public void AddingItems()
        {
            Set<int> intSet = new Set<int>(_defaultCollection);
            intSet.Add(10);
            intSet.Add(94);

            Assert.AreEqual(8, intSet.Count);
            Assert.AreEqual(true, intSet.Contains(10));
        }

        [TestMethod]
        public void Clear()
        {
            Set<int> intSet = new Set<int>(_defaultCollection);
            Set<int> emptySet = new Set<int>();
            intSet.Clear();

            Assert.AreEqual(0, intSet.Count);
            Assert.AreEqual(true, intSet.SetEquals(emptySet));
        }

        [TestMethod]
        public void ContainsItem()
        {
            Set<int> intSet = new Set<int>(_defaultCollection);

            Assert.AreEqual(false, intSet.Contains(0));
            Assert.AreEqual(true, intSet.Contains(3));
        }

        [TestMethod]
        public void RemovingItems()
        {
            Set<int> intSet = new Set<int>(_defaultCollection);
            intSet.Remove(2);
            intSet.Remove(1);

            Assert.AreEqual(4, intSet.Count);
            Assert.AreEqual(false, intSet.Contains(2));
        }

        [TestMethod]
        public void UnionWith()
        {
            Set<int> fillIntSet = new Set<int>(_defaultCollection);
            Set<int> intSet = new Set<int>();
            intSet.UnionWith(_defaultCollection);

            Assert.AreEqual(true, fillIntSet.SetEquals(intSet));
        }

        [TestMethod]
        public void IntersectWith()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(new[] { 1, 3, 5, 7, 8, 9 });
            Set<int> intersectedSet = new Set<int>(new[] { 1, 3, 5 });
            firstSet.IntersectWith(secondSet);

            Assert.AreEqual(true, firstSet.SetEquals(intersectedSet));
        }

        [TestMethod]
        public void ExceptWith()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(new[] { 1, 3, 5, 7, 8, 9 });
            Set<int> exceptedSet = new Set<int>(new[] { 2, 4, 6 });
            firstSet.ExceptWith(secondSet);

            Assert.AreEqual(true, firstSet.SetEquals(exceptedSet));
        }

        [TestMethod]
        public void SymmetricExceptWith()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(new[] { 1, 3, 5, 7, 8, 9 });
            Set<int> symmetricExceptedSet = new Set<int>(new[] { 2, 4, 6, 7, 8, 9 });
            firstSet.SymmetricExceptWith(secondSet);

            Assert.AreEqual(true, firstSet.SetEquals(symmetricExceptedSet));
        }

        [TestMethod]
        public void IsSubsetOf()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> superset = new Set<int>(_defaultCollection);
            superset.Add(10);
            superset.Add(20);

            Assert.AreEqual(true, firstSet.IsSubsetOf(superset));

            firstSet.Add(30);

            Assert.AreEqual(false, firstSet.IsSubsetOf(superset));

            firstSet.Clear();

            Assert.AreEqual(true, firstSet.IsSubsetOf(superset));
        }

        [TestMethod]
        public void IsSupersetOf()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> subset = new Set<int>(new[] { 1, 2, 6 });

            Assert.AreEqual(true, firstSet.IsSupersetOf(subset));

            subset.Add(100);

            Assert.AreEqual(false, firstSet.IsSupersetOf(subset));

            subset.Clear();

            Assert.AreEqual(true, firstSet.IsSupersetOf(subset));
        }

        [TestMethod]
        public void IsProperSubsetOf()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(_defaultCollection);

            Assert.AreEqual(false, firstSet.IsProperSubsetOf(secondSet));

            firstSet.Remove(1);

            Assert.AreEqual(true, firstSet.IsProperSubsetOf(secondSet));

            firstSet.Add(1);
            firstSet.Add(100);

            Assert.AreEqual(false, firstSet.IsProperSubsetOf(secondSet));
        }

        [TestMethod]
        public void IsProperSupersetOf()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(_defaultCollection);

            Assert.AreEqual(false, firstSet.IsProperSupersetOf(secondSet));

            secondSet.Remove(1);

            Assert.AreEqual(true, firstSet.IsProperSupersetOf(secondSet));

            secondSet.Add(1);
            secondSet.Add(100);

            Assert.AreEqual(false, firstSet.IsProperSupersetOf(secondSet));
        }

        [TestMethod]
        public void Overlaps()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(new[] { 1, 40, 90, 10 });

            Assert.AreEqual(true, firstSet.Overlaps(secondSet));

            secondSet.Remove(1);

            Assert.AreEqual(false, firstSet.Overlaps(secondSet));

            secondSet.Clear();

            Assert.AreEqual(false, firstSet.Overlaps(secondSet));

            firstSet.Clear();

            Assert.AreEqual(false, firstSet.Overlaps(secondSet));
        }

        [TestMethod]
        public void SetEquals()
        {
            Set<int> firstSet = new Set<int>(_defaultCollection);
            Set<int> secondSet = new Set<int>(_defaultCollection);

            Assert.AreEqual(true, firstSet.SetEquals(secondSet));

            secondSet.Remove(1);

            Assert.AreEqual(false, firstSet.SetEquals(secondSet));
        }
    }
}