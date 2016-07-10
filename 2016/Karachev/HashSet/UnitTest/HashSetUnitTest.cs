using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashSet;

namespace UnitTest
{
    [TestClass]
    public class HashSetUnitTest
    {
        private static Random rnd = new Random();

        [TestMethod]
        public void AddTest()
        {
            var set = new HashSet<int>();
            var limit = rnd.Next(1, 100);
            limit = 100;
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }
            Assert.IsFalse(set.Add(0));
            Assert.AreEqual(set.Count, limit);
            Assert.IsTrue(set.Add(limit + 1));
            Assert.AreEqual(set.Count, limit + 1);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var set = new HashSet<int>();
            var limit = rnd.Next(1, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }

            Assert.IsFalse(set.Remove(limit + 1));
            Assert.AreEqual(set.Count, limit);
            Assert.IsTrue(set.Remove(0));
            Assert.AreEqual(set.Count, limit - 1);
        }

        [TestMethod]
        public void ContainsTest()
        {
            var set = new HashSet<int>();
            var limit = rnd.Next(1, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }

            for (int i = 0; i < limit; i++)
            {
                Assert.IsTrue(set.Contains(i));
            }
            Assert.IsFalse(set.Contains(limit + 1));
        }

        [TestMethod]
        public void ClearTest()
        {
            var set = new HashSet<int>();
            var limit = rnd.Next(1, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }
            set.Clear();
            Assert.AreEqual(set.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToWithSmallArrayTest()
        {
            var set=new HashSet<int>();
            var arr = new int[1];
            var limit = rnd.Next(10, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }
            set.CopyTo(arr, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToWithLargeArrayIndexTest()
        {
            var set = new HashSet<int>();
            var arr = new int[1];
            var limit = rnd.Next(10, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }
            set.CopyTo(arr, arr.Length+1);
        }

        [TestMethod]
        public void CopyToTest()
        {
            var set = new HashSet<int>();
            var limit = rnd.Next(10, 100);
            for (int i = 0; i < limit; i++)
            {
                set.Add(i);
            }
            var arr = new int[10+limit];
            for (int i = 0; i < 10; i++)
            {
                arr[i] = -i-1;
            }

            set.CopyTo(arr, 10);
            for (int i = 10; i < arr.Length; i++)
            {
                Assert.AreEqual(arr[i],i-10);
            }
        }

        [TestMethod]
        public void UnionWithTest()
        {           
            var set=new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = 90 + i;
            }
            set.UnionWith(arr);
            Assert.AreEqual(set.Count, 190);
            foreach (int item in arr)
            {
                Assert.IsTrue(set.Contains(item));
            }
        }

        [TestMethod]
        public void IntersectWithTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = 90 + i;
            }
            set.IntersectWith(arr);
            Assert.AreEqual(set.Count, 10);
            for (int i = 90; i < 100; i++)
            {
                Assert.IsTrue(set.Contains(i));
            }
        }

        [TestMethod]
        public void IntersectWithEmptyResultTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = 900 + i;
            }
            set.IntersectWith(arr);
            Assert.AreEqual(set.Count, 0);
        }

        [TestMethod]
        public void IntersectWithFullResultTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = i;
            }
            set.IntersectWith(arr);
            Assert.AreEqual(set.Count, 100);
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(set.Contains(arr[i]));
            }
        }

        [TestMethod]
        public void ExceptWithTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = i-10;
            }
            set.ExceptWith(arr);
            Assert.AreEqual(set.Count, 10);
            for (int i = 90; i < 100; i++)
            {
                Assert.IsTrue(set.Contains(i));
            }
        }

        [TestMethod]
        public void ExceptWithEmptyResultTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = i;
            }
            set.ExceptWith(arr);
            Assert.AreEqual(set.Count, 0);
        }

        [TestMethod]
        public void ExceptWithFullResultTest()
        {
            var set = new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = i +100;
            }
            set.ExceptWith(arr);
            Assert.AreEqual(set.Count, 100);
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(set.Contains(i));
            }
        }

        [TestMethod]
        public void SymmetricExceptWithTest()
        {
            var set=new HashSet<int>();
            var arr = new int[100];
            for (int i = 0; i < 100; i++)
            {
                set.Add(i);
                arr[i] = i + 50;
            }
            set.SymmetricExceptWith(arr);
            Assert.AreEqual(set.Count, 100);
            for (int i = 0,j=100; i < 50 && j<150; i++,j++)
            {
                Assert.IsTrue(set.Contains(i) && set.Contains(j));
            }
        }

        [TestMethod]
        public void IsSubsetOfFailureTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            for (int i = 0; i < 5; i++)
            {
                set.Add(i+8);
            }

            Assert.IsFalse(set.IsSubsetOf(arr));
        }

        [TestMethod]
        public void IsSubsetOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            for (int i = 0; i < 5; i++)
            {
                set.Add(i);
            }

            Assert.IsTrue(set.IsSubsetOf(arr));
        }

        [TestMethod]
        public void IsSubsetOfEmptySetTest()
        {
            var set = new HashSet<int>();
            Assert.IsTrue(set.IsSubsetOf(Enumerable.Range(0, 10)));
        }

        [TestMethod]
        public void IsSubsetOfEqualSetsTest()
        {
            var set = new HashSet<int>(Enumerable.Range(0, 10));
            Assert.IsTrue(set.IsSubsetOf(Enumerable.Range(0,10)));
        }

        [TestMethod]
        public void IsSupersetOfFailureTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            for (int i = 0; i < 5; i++)
            {
                set.Add(i);
            }

            Assert.IsFalse(set.IsSupersetOf(arr));
        }

        [TestMethod]
        public void IsSupersetOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            for (int i = 0; i < 50; i++)
            {
                set.Add(i -25);
            }

            Assert.IsTrue(set.IsSupersetOf(arr));
        }

        [TestMethod]
        public void IsSupersetOfEqualsSetsTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            set.UnionWith(arr);
            Assert.IsTrue(set.IsSupersetOf(arr));
        }

        [TestMethod]
        public void IsSupersetOfEmptyCollectionTest()
        {
            var set = new HashSet<int>(Enumerable.Range(0,10));
            Assert.IsTrue(set.IsSupersetOf(Enumerable.Range(0,0)));
        }

        [TestMethod]
        public void IsProperSubsetFailureOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            set.UnionWith(arr);
            Assert.IsFalse(set.IsProperSubsetOf(arr));
        }

        [TestMethod]
        public void IsProperSubsetOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            set.UnionWith(arr);
            set.Remove(0);
            Assert.IsTrue(set.IsProperSubsetOf(arr));
        }

        [TestMethod]
        public void IsProperSubsetEmptySetOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            Assert.IsTrue(set.IsProperSubsetOf(arr));
        }

        [TestMethod]
        public void IsProperSupersetFailureOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            set.UnionWith(arr);
            Assert.IsFalse(set.IsProperSupersetOf(arr));
        }

        [TestMethod]
        public void IsProperSupersetOfTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10).ToArray();
            set.UnionWith(arr);
            set.Add(11);
            Assert.IsTrue(set.IsProperSupersetOf(arr));
        }

        [TestMethod]
        public void IsProperSupersetOfEmptyArr()
        {
            var arr = new int[0];
            var set=new HashSet<int>();
            set.Add(0);
            Assert.IsTrue(set.IsProperSupersetOf(arr));
        }

        [TestMethod]
        public void OverlapsFailureTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10);
            set.UnionWith(Enumerable.Range(11, 20));
            Assert.IsFalse(set.Overlaps(arr));
        }

        [TestMethod]
        public void OverlapsTest()
        {
            var set=new HashSet<int>();
            var arr = Enumerable.Range(0, 10);
            set.UnionWith(Enumerable.Range(8, 10));
            Assert.IsTrue(set.Overlaps(arr));
        }

        [TestMethod]
        public void SetEqualsFailureTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10);
            set.UnionWith(Enumerable.Range(1, 10));
            Assert.IsFalse(set.SetEquals(arr));
        }

        [TestMethod]
        public void SetEqualsTest()
        {
            var set = new HashSet<int>();
            var arr = Enumerable.Range(0, 10);
            set.UnionWith(Enumerable.Range(0, 10));
            Assert.IsTrue(set.SetEquals(arr));
        }

        [TestMethod]
        public void EnumerableEmptySetTest()
        {
            var set=new HashSet<int>();
            foreach (var item in set)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EnumerableTest()
        {
            var set=new HashSet<int>();
            set.UnionWith(Enumerable.Range(0, 10));
            foreach (var item in set)
            {
                Assert.IsTrue(set.Contains(item));
            }

        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void EnumerableImmutabilityTest()
        {
            var set=new HashSet<int>();
            set.UnionWith(Enumerable.Range(0, 10));
            foreach (var item in set)
            {
                set.Add(100);
            }
        }

        [TestMethod]
        public void RemoveWhereTest()
        {
            var set = new HashSet<int>(Enumerable.Range(0, 100));
            set.RemoveWhere(a => a%2 == 0);
            Assert.AreEqual(set.Count, 50);
            for (int i = 1; i < 100; i+=2)
            {
                Assert.IsTrue(set.Contains(i));
            }
        }

        [TestMethod]
        public void ImmutabilityAfterTrimExcessTest()
        {
            var set = new HashSet<int>(Enumerable.Range(0, 100));
            set.TrimExcess();
            Assert.AreEqual(set.Count, 100);
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(set.Contains(i));
            }
        }

        [TestMethod]
        public void EmptySetTrimExcessTest()
        {
            var set=new HashSet<int>();
            set.TrimExcess();
            Assert.AreEqual(set.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnumerableBreakAfterTrimExcess()
        {
            var set = new HashSet<int>(Enumerable.Range(0, 10));
            foreach (var item in set)
            {
                set.TrimExcess();
            }
        }
    }
}
