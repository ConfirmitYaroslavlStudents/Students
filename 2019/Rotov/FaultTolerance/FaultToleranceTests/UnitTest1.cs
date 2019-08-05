using FaultTolerance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading;

namespace FaultToleranceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ActionFallBackWork()
        {
            int count = 15;
            Action Devide = () => { count = count / 0; };
            Action Fix = () => { count = 0; };
            ToleranceLibrary.FallBack<DivideByZeroException>(Devide, Fix, 1000);
            Assert.AreEqual(count, 0);
        }

        [TestMethod]
        public void ActionFallBackWithTimeOutWork()
        {
            var flag = false;
            Action Delay = () => Thread.Sleep(5000);
            Action Handle = () => flag = true;
            ToleranceLibrary.FallBack<IOException>(Delay, Handle, 3000);
            Assert.IsTrue(flag);
        }


        [TestMethod]
        public void FunkFallBackWork()
        {
            int count = 0;
            Func<int, int> Payload = delegate (int val)
            {
                throw new ArgumentNullException();
            };
            Func<int, int> Spare = delegate (int val)
            {
                return val;

            };
            count = ToleranceLibrary.FallBack<ArgumentNullException, int, int>(Payload, Spare, 1, 1000);
            Assert.AreEqual(count, 1);
        }

        [TestMethod]
        public void FunkFallBackWithTimeOutWork()
        {
            int count;
            Func<int, int> Payload = (int val) => { Thread.Sleep(1100); return 1; };
            Func<int, int> Spare = (int val) => { return 2; };
            count = ToleranceLibrary.FallBack<DivideByZeroException, int, int>(Payload, Spare, 0, 1000);
            Assert.AreEqual(count, 2);
        }

        [TestMethod]

        public void ActionRetryWork()
        {
            int count = 15;
            Action Devide = () => { count--; count = count / 0; };
            ToleranceLibrary.Retry<DivideByZeroException>(Devide, 5, 2000);
            Assert.AreEqual(count, 10);
        }

        [TestMethod]

        public void ActionRetryWithTimeOutWokr()
        {
            int count = 0;
            Action Payload = () => { Thread.Sleep(10000); count++; };
            ToleranceLibrary.Retry<DivideByZeroException>(Payload, 3, 500);
            Assert.AreEqual(count, 0);
        }

        [TestMethod]

        public void FunkRetryWithTimeOutWork()
        {
            Func<int, int> Payload = (int val) => { Thread.Sleep(2000); return 1; };
            Action result = () => { int v = ToleranceLibrary.Retry<DivideByZeroException, int, int>(Payload, 5, 5, 100); };
            Assert.ThrowsException<FormatException>(result);
        }
    }
}
