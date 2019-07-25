using System;
using Xunit;

namespace FaultToleranceTests
{
    public class Tests
    {
        [Fact]
        public void RetryAction_Succeed_NoExceptions()
        {
            int value = 0;

            Action action = () =>
            {
                if (value < 4)
                {
                    value++;
                    throw new ArithmeticException();
                }
                else return;
            };

            FaultTolerance.FaultTolerance.Retry<ArithmeticException>(action, 6);
            Assert.Equal(4, value);
        }
        [Fact]
        public void RetryAction_Failed_CalledException()
        {
            int value = 0;

            Action action = () =>
            {
                if (value < 7)
                {
                    value++;
                    throw new ArithmeticException();
                }
                else return;
            };

            Assert.Throws<ArithmeticException>(() => FaultTolerance.FaultTolerance.Retry<ArithmeticException>(action, 6));
        }
        [Fact]
        public void RetryFunc_Succeed_NoExceptions()
        {
            int expected = 0;

            Func<int> func = () =>
            {
                if (expected < 4)
                {
                    expected++;
                    throw new ArithmeticException();
                }
                else return 123;
            };

            Assert.Equal(123, FaultTolerance.FaultTolerance.Retry<ArithmeticException, int>(func, 6));
        }
        [Fact]
        public void RetryFunc_Failed_CalledException()
        {
            int expected = 0;

            Func<int> func = () =>
            {
                if (expected < 7)
                {
                    expected++;
                    throw new ArithmeticException();
                }
                else return 123;
            };

            Assert.Throws<ArithmeticException>(() => FaultTolerance.FaultTolerance.Retry<ArithmeticException, int>(func, 6));
        }

        [Fact]
        public void FallbackAction_CalledException_CalledSecondMethod()
        {
            int expected = 0;

            Action first = () => throw new NotImplementedException();
            Action second = () => expected++;

            FaultTolerance.FaultTolerance.FallBack<NotImplementedException>(first, second);
            Assert.Equal(1, expected);
        }
        [Fact]
        public void FallbackAction_Succeed_NoExseption()
        {
            int expected = 0;

            Action first = () => expected = 4; 
            Action second = () => expected++;

            FaultTolerance.FaultTolerance.FallBack<NotImplementedException>(first, second);
            Assert.Equal(4, expected);
        }
        [Fact]
        public void FallbackFunc_CalledException_CalledSecondMethod()
        {

            Func<int> first = () => throw new NotImplementedException();
            Func<int> second = () => 1;

            int value = FaultTolerance.FaultTolerance.FallBack<NotImplementedException,int>(first, second);
            Assert.Equal(1, value);
        }
        [Fact]
        public void FallbackFunc_Succeed_NoExceptions()
        {

            Func<int> first = () => 5;
            Func<int> second = () => 1;

            int value = FaultTolerance.FaultTolerance.FallBack<NotImplementedException, int>(first, second);
            Assert.Equal(5, value);
        }
    }
}
