using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FaultTolerance;
using System.IO;

namespace FaultToleranceTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExceptIsCathing1()
        {
            int count = 15;
            Action Devide = () => { count = count / 0; };
            Action Fix = () => { count = 0; };
            ToleranceLibrary.FallBack<DivideByZeroException>(Devide, Fix);
            Assert.AreEqual(count, 0);
        }

        [TestMethod]

        public void ExceptIsCathing2()
        {
            int count = 15;
            Action Devide = () => { count = count / 0; };
            Action Fix = () => { count = 5; };
            ToleranceLibrary.Retry<DivideByZeroException>(Devide, Fix, 5);
            Assert.AreEqual(count, 5);
        }

        [TestMethod]

        public void MethodIsRepeating()
        {
            int count = 0;
            Action Add = () => { count++;
                var zero = 0;
                var curr = 10 / zero; };
            Action Fix = () => { return; };
            ToleranceLibrary.Retry<DivideByZeroException>(Add, Fix, 5);
            Assert.AreEqual(count, 5);
        }

        [TestMethod]

        public void PlanBIsWorking()
        {
            bool flag = false;
            Action Writting = () =>
            {
                using (StreamReader r = new StreamReader("file.txt"))
                {
                    var data = r.ReadToEnd();
                };

            };

            Action Another = () => flag = true;
            ToleranceLibrary.Retry<IOException>(Writting, Another, 3);
            Assert.AreEqual(flag, true);
        }

        [TestMethod]

        public void TestReturnValue1()
        {
            int count = 0;
            Func<int, int> Multiply = x => { count++; return checked(x * x); };
            Func<int, int> Test = x => x + x;
            var res = ToleranceLibrary.Retry<OverflowException, int, int>(Multiply, Test, 1000000, 3);
            Assert.AreEqual(count, 3);
        }

        [TestMethod]

        public void TestReturnValue2()
        {
            Func<int, int> Multiply = x => { return checked(x * x); };
            Func<int, int> Modul = x => { return x % 2; };
            var result =  ToleranceLibrary.FallBack<OverflowException, int, int>(Multiply, Modul, 1000000);
            Assert.AreEqual(result, 0);
        }
    }
}
