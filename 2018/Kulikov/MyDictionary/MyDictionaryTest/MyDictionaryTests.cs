using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDictionary;

namespace MyDictionaryTest
{
    [TestClass]
    public class MyDictionaryTests
    {
        [TestMethod]
        public void Add_4_objects()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            int actual = MD.Count;
            int expected = 4;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Remove_2_objects()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            MD.Remove(1);
            MD.Remove(4);

            int actual = MD.Count;
            int expected = 2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Contains_3()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            bool actual = MD.ContainsKey(3);
            bool expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void NotContains_10()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            bool actual = MD.ContainsKey(10);
            bool expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValue_ByKey()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            char actual = MD[4];
            char expected = 'd';

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_ExistingKey()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(3, 'd');
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValue_ByNotExistingKey()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            char actual = MD[5];
        }

        [TestMethod]
        public void ChangeValue_ByKey()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            MD[4] = 'x';

            char actual = MD[4];
            char expected = 'x';

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveItem_and_GetValue()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');
            MD.Remove(1);

            char actual = MD[1];
        }

        [TestMethod]
        public void NotContains_RemovedItem()
        {
            MyDictionary<int, char> MD = new MyDictionary<int, char>();
            MD.Add(1, 'a');
            MD.Add(2, 'b');
            MD.Add(3, 'c');
            MD.Add(4, 'd');

            MD.Remove(2);

            bool actual = MD.ContainsKey(2);
            bool expected = false;

            Assert.AreEqual(expected, actual);
        }
    }
}
