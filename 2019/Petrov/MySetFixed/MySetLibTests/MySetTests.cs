using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySetLib;
namespace MySetLibTests
{
    [TestClass]
    public class MySetTests
    {
        [TestMethod]
        public void FindIfContain()
        {
            Set<int> actualSet = new Set<int>();
            int expected = 1;

            actualSet.Add(1);
            Set<int>.Node<int> actual = actualSet.Find(1);

            Assert.AreEqual(actual.Data, expected);
        }

        [TestMethod]
        public void FindIfCountIsZero()
        {
            Set<int> actualSet = new Set<int>();
            Set<int>.Node<int> expected = null;

            Set<int>.Node<int> actual = actualSet.Find(1);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AddIncreaseCount()
        {
            Set<int> actualSet = new Set<int>();
            int expected = 1;

            actualSet.Add(1);

            Assert.AreEqual(actualSet.count, expected);
        }

        [TestMethod]
        public void AddIfSetAlreadyContatinData()
        {
            Set<int> actualSet = new Set<int>();
            actualSet.Add(1);
            int expected = 1;

            actualSet.Add(1);

            Assert.AreEqual(actualSet.count, expected);
        }

        [TestMethod]
        public void Add()
        {
            Set<int> actualSet = new Set<int>();
            int expected = 2;

            actualSet.Add(1);
            actualSet.Add(2);

            Assert.AreEqual(actualSet.Find(2).Data, expected);
        }

        [TestMethod]
        public void RemoveIfSetdontContain()
        {
            Set<int> actualSet = new Set<int>();
            bool expected = false;
            actualSet.Add(1);
            actualSet.Add(2);

            bool actual = actualSet.Remove(3);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RemoveFirst()
        {
            Set<int> actualSet = new Set<int>();
            bool expected = true;
            actualSet.Add(1);

            bool actual = actualSet.Remove(1);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void RemoveDecreaseCount()
        {
            Set<int> actualSet = new Set<int>();
            int expected = 5;

            actualSet.Add(3);
            actualSet.Add(4);
            actualSet.Add(1);
            actualSet.Add(2);
            actualSet.Add(5);
            actualSet.Add(6);
            actualSet.Remove(3);

            Assert.AreEqual(actualSet.count, expected);
        }

        [TestMethod]
        public void Union()
        {
            Set<int> actual = new Set<int>();
            Set<int> a = new Set<int>();
            Set<int> b = new Set<int>();
            int expected = 6;

            a.Add(3);
            a.Add(4);
            a.Add(1);
            b.Add(2);
            b.Add(5);
            b.Add(6);
            actual = a.Union(b);

            Assert.AreEqual(actual.count, expected);
        }

        [TestMethod]
        public void Intersection()
        {
            Set<int> actual = new Set<int>();
            Set<int> a = new Set<int>();
            Set<int> b = new Set<int>();
            Set<int> expected = new Set<int>();
            expected.Add(3);
            expected.Add(2);

            a.Add(1);
            a.Add(2);
            a.Add(3);

            b.Add(2);
            b.Add(3);
            b.Add(6);
            actual = a.Intersection(b);

            Assert.AreEqual(actual.count, expected.count);
        }
    }
}
