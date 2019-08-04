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
        public void Try_FlagIsFalse_ReturnFlagIsTrue()
        {
            bool flag = false;
            Action action = () => flag = true;
            FaultTolerance faultTolerance = new FaultTolerance();

            faultTolerance.Try(action, 1);

            Assert.AreEqual(true, flag);
        }

        [TestMethod]
        public void Fallback_FlagIsFalse_ReturnflagIsTrue()
        {
            bool flag = false;

            Action action = () => throw new ContextMarshalException();
            Action fallback = () => flag = true;
            FaultTolerance faultTolerance = new FaultTolerance();
            faultTolerance.Fallback(action, fallback);

            Assert.AreEqual(true,flag);
        }

        [TestMethod]
        public void Try_ActionThrowsException_true()
        {
            Action action = () => throw new IndexOutOfRangeException();
            FaultTolerance faultTolerance = new FaultTolerance();

            Assert.ThrowsException<IndexOutOfRangeException>(() => faultTolerance.Try(action, 2));
        }

        [TestMethod]
        public void Try_StringsAreEqual_false()
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
