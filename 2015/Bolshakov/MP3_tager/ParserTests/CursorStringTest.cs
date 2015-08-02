using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mp3Handler;

namespace Tests
{
    [TestClass]
    public class CursorStringTest
    {
        [TestMethod]
        public void IncrementTest()
        {
            var str = new StrWithCursor("test");
            Assert.AreEqual('t',str.Value);
            str++;
            Assert.AreEqual('e', str.Value);
            str++;
            Assert.AreEqual('s', str.Value);
            str++;
            Assert.AreEqual('t', str.Value);
        }

        [TestMethod]
        public void NonResultIncrementTest()
        {
            var str = new StrWithCursor("te");
            Assert.AreEqual('t', str.Value);
            str++;
            Assert.AreEqual('e', str.Value);
            str++;
            Assert.AreEqual('e', str.Value);
        }

        [TestMethod]
        public void EqualOperatorTest()
        {
            var str1 = new StrWithCursor("te");
            var str2 = new StrWithCursor("te");
            Assert.AreEqual(true,str1==str2);
            str2++;
            Assert.AreEqual(false, str1 == str2);
        }

        [TestMethod]
        public void EqualWithCharOperatorTest()
        {
            var str1 = new StrWithCursor("t");
            var symbol = 't';
            Assert.AreEqual(true, str1 == symbol);
            symbol = 'e';
            Assert.AreEqual(false, str1 == symbol);
        }

        [TestMethod]
        public void CopyTest()
        {
            var str1 = new StrWithCursor("te");
            var str2 = new StrWithCursor(str1);
            str2++;
            Assert.AreEqual(true, str1 != str2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NotValidStringException()
        {
            new StrWithCursor("");
        }
    }
}
