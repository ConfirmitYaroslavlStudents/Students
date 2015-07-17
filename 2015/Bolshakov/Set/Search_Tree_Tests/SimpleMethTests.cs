using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetLib;
using System.Collections.Generic;

namespace Search_Tree_Tests
{
    [TestClass]
    public class SimpleMethTests
    {
        //[TODO] remove random
        [TestMethod]
        public void CountTest()
        {
            var rnd = new Random();

            var tree = new Set<int>();
            var added = rnd.Next(100);
            var deleted = rnd.Next(added);
            var expect = added - deleted;
            var arr = new int[added];

            for (int i = 0; i < added; i++)
            {
                tree.Add(i);
                arr[i] = i;
            }

            for (int i = 0; i < deleted; i++)
            {
                tree.Remove(arr[added - i - 1]);
            }

            Assert.AreEqual(expect, tree.Count);
        }

        [TestMethod]
        public void RndCountTest()
        {
            var set = new SortedSet<int>();
            var tree = new Set<int>();
            var rnd = new Random();

            for (int i = 0; i < rnd.Next(1000); i++)
            {
                var num = rnd.Next(1000);
                set.Add(num);
                tree.Add(num);
            }

            var arr = new int[set.Count];
            set.CopyTo(arr);

            var toDelete = rnd.Next(set.Count);

            for (int i = 0; i < toDelete; i++)
            {
                tree.Remove(arr[i]);
            }

            Assert.AreEqual(set.Count - toDelete, tree.Count);
        }

        [TestMethod]
        public void AddTest()
        {
            var set = new Set<int>();
            var list = new List<int>();
            var except=true;
            bool result=true;

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
                list.Add(i);
            }

            foreach (var item in list)
            {
                if (!set.Contains(item))
                {
                    result = false;
                }
            }

            Assert.AreEqual(except, result);
        }

        [TestMethod]
        public void AddModifiedExcepTest()
        {
            var set = new Set<int>();

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }
            try
            {
                foreach (var item in set)
                {
                     set.Add(1001);
                }
            }
            catch (InvalidOperationException ex)
            {
                StringAssert.Contains(ex.Message, "Collection was modified");
            }
        }

        [TestMethod]
        public void ClearTest()
        {
            var set = new Set<int>();
            var except = true;
            var result = true;

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }

            set.Clear();

            foreach (var item in set)
            {
                result = false;
                break;
            }

            Assert.AreEqual(except, result);
        }

        [ExpectedException(typeof(InvalidOperationException),"Collection was modified")]
        public void ClearModifiedExcepTest()
        {
            var set = new Set<int>();

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }
            
            foreach (var item in set)
            {
                set.Clear();
            }
        }

        [TestMethod]
        public void PlusContainsTest()
        {
            var rnd = new Random();
            var set = new Set<int>();
            var except = true;
            var result = true;

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }

            result = set.Contains(rnd.Next(1000));

            Assert.AreEqual(except, result);
        }

        [TestMethod]
        public void MinusContainsTest()
        {
            var rnd = new Random();
            var set = new Set<int>();
            var except = false;
            var result = true;

            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }

            result = set.Contains(1000+rnd.Next(1000));

            Assert.AreEqual(except, result);
        }

        [TestMethod]
        public void CopyToArreyTest()
        {
            var set = new Set<int>();
            var sourceArr = new int[1000];

            for (int i = 0; i < 1000; i++)
            {
                sourceArr[i] = i;
                set.Add(i);
            }

            var resultArr = new int[1000];
            set.CopyTo(resultArr, 0);

            Array.Sort(resultArr);

            Assert.AreEqual(999, resultArr[999]);
        }

        [TestMethod]
        public void ExceptTest()
        {
            var rnd = new Random();
            var set = new Set<int>();
            var first = new List<int>();
            var second = new Set<int>();

            var treshhold = rnd.Next(999);
            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
                if (i < treshhold)
                    first.Add(i);
                else
                    second.Add(i);
            }

            set.ExceptWith(first);

            var result = set.Equals(second);

            Assert.AreEqual(true, result);
        }
    }
}
