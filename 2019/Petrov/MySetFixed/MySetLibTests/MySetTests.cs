using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySetLib;
namespace MySetLibTests
{
    [TestClass]
    public class MySetTests
    {
        [TestMethod]
        public void Find_SearchSetInNonEptyList_FindElement()
        {
            Set<int> actualSet = new Set<int>();

            actualSet.Add(1);
            
            Assert.AreEqual(true, actualSet.Contains(1));
        }

        [TestMethod]
        public void Find_SearchElementInSetList_FindNothing()
        {
            Set<int> actualSet = new Set<int>();

            Assert.AreEqual(false, actualSet.Contains(1));
        }

        [TestMethod]
        public void Add_AddElementToEmptySet_IncreaseCountToOne()
        {
            Set<int> actualSet = new Set<int>();

            actualSet.Add(1);

            Assert.AreEqual(1, actualSet.count);
        }

        [TestMethod]
        public void Add_AddElementToSetIfElementAlreadyExist_CountIsStillOne()
        {
            Set<int> actualSet = new Set<int>();
            actualSet.Add(1);

            actualSet.Add(1);

            Assert.AreEqual(1, actualSet.count);
        }

        [TestMethod]
        public void Add_AddElementsToEmptySet_CountIsTwo()
        {
            Set<int> actualSet = new Set<int>();

            actualSet.Add(1);
            actualSet.Add(2);

            Assert.AreEqual(2, actualSet.count);
        }

        [TestMethod]
        public void Remove_SetdontContainSuchElement_CountIsStillTwo()
        {
            Set<int> actualSet = new Set<int>();

            actualSet.Add(1);
            actualSet.Add(2);

            Assert.AreEqual(false, actualSet.Remove(3));
        }

        [TestMethod]
        public void Remove_SetHasOneElement_CountIsZero()
        {
            Set<int> actualSet = new Set<int>();
            actualSet.Add(1);

            actualSet.Remove(1);

            Assert.AreEqual(0, actualSet.count);
        }

        [TestMethod]
        public void Remove_SetHasSixElements_DecreaseCountToFive()
        {
            Set<int> actualSet = new Set<int>();

            actualSet.Add(3);
            actualSet.Add(4);
            actualSet.Add(1);
            actualSet.Add(2);
            actualSet.Add(5);
            actualSet.Add(6);
            actualSet.Remove(3);

            Assert.AreEqual(5, actualSet.count);
        }

        [TestMethod]
        public void Union_TwoSetsWithSimilarElements_CountIsFive()
        {
            Set<int> a = new Set<int>();
            Set<int> b = new Set<int>();

            a.Add(3);
            a.Add(4);
            a.Add(1);
            b.Add(2);
            b.Add(5);
            b.Add(6);

            Assert.AreEqual(6, a.Union(b).count);
        }

        [TestMethod]
        public void Intersection_TwoSetsWithTwoSimilarElements_CountIsTwo()
        {
            Set<int> a = new Set<int>();
            Set<int> b = new Set<int>();

            a.Add(1);
            a.Add(2);
            a.Add(3);

            b.Add(2);
            b.Add(3);
            b.Add(6);
            
            Assert.AreEqual(2, a.Intersection(b).count);
        }
    }
}
