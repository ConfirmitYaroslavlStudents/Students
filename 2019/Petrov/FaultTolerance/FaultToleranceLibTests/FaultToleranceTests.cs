using System;
using System.Text;
using FaultToleranceLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FaultToleranceLibTests
{
    [TestClass]
    public class FaultToleranceTests
    {
        [TestMethod]
        public void Try_Square_true()
        {
            Random rand = new Random();
            int actual = rand.Next(100);
            int expected = actual * actual;
            Action action = () =>
            {
                actual *= actual;
            };
            FaultTolerance faultTolerance = new FaultTolerance();

            faultTolerance.Try(action, 1);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Try_Fallback_true()
        {
            int IsFallback = 0;

            Action action = () => throw new ContextMarshalException();
            Action fallback = () => IsFallback = 1;
            FaultTolerance faultTolerance = new FaultTolerance();
            faultTolerance.Fallback(action, fallback);

            Assert.AreEqual(1,IsFallback);
        }

        [TestMethod]
        public void Try_SquareFallback_false()
        {
            Random rand = new Random();
            int actual = rand.Next(100);
            int expected = actual * actual;

            int IsFallback = 0;
            Action fallBack = () => IsFallback = 1;

            Action action = () =>
            {
                actual *= actual;
            };
            FaultTolerance faultTolerance = new FaultTolerance();

            faultTolerance.Fallback(action, fallBack);
            Assert.AreEqual(0, IsFallback);
        }

        [TestMethod]
        public void Try_indexOutOfRangeEx_true()
        {
            Action action = () => throw new IndexOutOfRangeException();

            FaultTolerance faultTolerance = new FaultTolerance();

            Assert.ThrowsException<IndexOutOfRangeException>(() => faultTolerance.Try(action, 2));
        }

        [TestMethod]
        public void Try_StringEquals_false()
        {
            StringBuilder actual = new StringBuilder();
            StringBuilder expected = new StringBuilder("Hello Somebody");
            Action action = () => throw new ArgumentException();
            Action fallBack = () =>
            {
                actual.Append("Hello Somebody");
            };
            FaultTolerance faultTolerance = new FaultTolerance();

            faultTolerance.Fallback(action, fallBack);

            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
