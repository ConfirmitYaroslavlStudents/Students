using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetLib;
using System.Collections.Generic;

namespace Search_Tree_Tests
{
    [TestClass]
    public class SimpleMethTests
    {
        //[TestMethod]
        //public void CountTest()
        //{
        //    var rnd = new Random();

        //    var tree = new Tree<int>();
        //    var added = rnd.Next(100);
        //    var deleted = rnd.Next(added);
        //    var expect = added - deleted;
        //    var arr = new int[added];

        //    for (int i = 0; i < added; i++)
        //    {
        //        tree.Add(i);
        //        arr[i] = i;
        //    }

        //    for (int i = 0; i < deleted; i++)
        //    {
        //        tree.Remove(arr[added - i - 1]);
        //    }

        //    Assert.AreEqual(expect, tree.Count);
        //}

        //[TestMethod]
        //public void RndCountTest()
        //{
        //    var set = new SortedSet<int>();
        //    var tree = new Tree<int>();
        //    var rnd = new Random();

        //    for (int i = 0; i < rnd.Next(1000); i++)
        //    {
        //        var num = rnd.Next(1000);
        //        set.Add(num);
        //        tree.Add(num);
        //    }

        //    var arr = new int[set.Count];
        //    set.CopyTo(arr);

        //    var toDelete = rnd.Next(set.Count);

        //    for (int i = 0; i < toDelete; i++)
        //    {
        //        tree.Remove(arr[i]);
        //    }

        //    Assert.AreEqual(set.Count - toDelete, tree.Count);
        //}
    }
}
