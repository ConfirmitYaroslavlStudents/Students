using System;
using System.Collections.Generic;
using Xunit;

namespace FaultToleranceLib.Tests
{
    public class FaultToleranceLibTests
    {
        [Fact]
        public void Try_Failed_ThrowException()
        {
            Action action = () => throw new NotImplementedException();

            Assert.Throws<NotImplementedException>(() => FaultTolerance.Try<NotImplementedException>(action, 2));
        }

        [Fact]
        public void Try_Succeed_NoExceptionThrown()
        {
            int count = 1;
            Action action = () =>
            {
                if (count < 3)
                {
                    count++;
                    throw new ArithmeticException();
                }
            };

            FaultTolerance.Try<ArithmeticException>(action, 3);
        }

        [Fact]
        public void TryCountTimes_ActionRunsCountTimes()
        {
            int actual = 0;

            Action action = () =>
            {
                actual++;
                if (actual < 3)
                    throw new NotImplementedException();
            };

            int expected = 3;

            FaultTolerance.Try<NotImplementedException>(action, 3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryFallback_Failed_RunFallback()
        {
            bool fallbackHadRun = false;

            Action action = () => throw new NotImplementedException();
            Action fallback = () => fallbackHadRun = true;

            FaultTolerance.TryFallback<NotImplementedException>(action, 2, fallback);

            Assert.True(fallbackHadRun);
        }

        [Fact]
        public void TryFallback_Succed_NoFallbackRun()
        {
            bool fallbackHadRun = false;

            int count = 1;
            Action action = () =>
            {
                if (count < 3)
                {
                    count++;
                    throw new ArithmeticException();
                }
            };

            Action fallback = () => fallbackHadRun = true;

            FaultTolerance.TryFallback<ArithmeticException>(action, 3, fallback);

            Assert.False(fallbackHadRun);
        }

        [Fact]
        public void TryFallback_Failed_NoFallbackRun_ThrowsException()
        {
            bool fallbackHadRun = false;

            int count = 1;
            Action action = () =>
            {
                if (count < 3)
                {
                    count++;
                    throw new ArithmeticException();
                }
                else
                    throw new ArgumentException();
            };
            Action fallback = () => fallbackHadRun = true;

            Assert.Throws<ArgumentException>(() => FaultTolerance.TryFallback<ArithmeticException>(action, 3, fallback));

            Assert.False(fallbackHadRun);
        }

        [Fact]
        public void TryMultipleExceptions_Succeed_NoExceptionThrown()
        {
            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new NotImplementedException();
                else if (count == 2)
                    throw new ArithmeticException();
            };

            var exceptions = new List<Type> { typeof(NotImplementedException), typeof(ArithmeticException) };

            FaultTolerance.Try(exceptions, action, 3);
        }

        [Fact]
        public void TryMultipleExceptions_Failed_ThrowsException()
        {
            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new NotImplementedException();
                else if (count == 2)
                    throw new ArithmeticException();
                else
                    throw new ArgumentException();
            };

            var exceptions = new List<Type> { typeof(NotImplementedException), typeof(ArithmeticException), typeof(ArgumentException) };

            Assert.Throws<ArgumentException>(() => FaultTolerance.Try(exceptions, action, 3));
        }

        [Fact]
        public void TryFallbackMultipleExceptions_Succeed_NoFallbackRun()
        {
            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new NotImplementedException();
                else if (count == 2)
                    throw new ArithmeticException();
            };

            bool notImplementedExceptionFallbackHadRun = false;
            bool arithmeticExceptionFallbackHadRun = false;

            var exceptions = new Dictionary<Type, Action>();
            exceptions.Add(typeof(NotImplementedException), () => notImplementedExceptionFallbackHadRun = true);
            exceptions.Add(typeof(ArithmeticException), () => arithmeticExceptionFallbackHadRun = true);

            FaultTolerance.TryFallback(exceptions, action, 3);

            Assert.False(notImplementedExceptionFallbackHadRun);
            Assert.False(arithmeticExceptionFallbackHadRun);
        }

        [Fact]
        public void TryFallbackMultipleExceptions_Failed_NoFallbackRun_ThrowsException()
        {
            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new NotImplementedException();
                else if (count == 2)
                    throw new ArithmeticException();
                else
                    throw new ArgumentException();
            };

            bool notImplementedExceptionFallbackHadRun = false;
            bool arithmeticExceptionFallbackHadRun = false;

            var exceptions = new Dictionary<Type, Action>();
            exceptions.Add(typeof(NotImplementedException), () => notImplementedExceptionFallbackHadRun = true);
            exceptions.Add(typeof(ArithmeticException), () => arithmeticExceptionFallbackHadRun = true);

            Assert.Throws<ArgumentException>(() => FaultTolerance.TryFallback(exceptions, action, 3));

            Assert.False(notImplementedExceptionFallbackHadRun);
            Assert.False(arithmeticExceptionFallbackHadRun);
        }

        [Fact]
        public void TryFallbackMultipleExceptions_Failed_RunRelevantFallback()
        {
            int count = 0;
            Action action = () =>
            {
                count++;
                if (count == 1)
                    throw new NotImplementedException();
                else if (count == 2)
                    throw new ArithmeticException();
            };

            bool notImplementedExceptionFallbackHadRun = false;
            bool arithmeticExceptionFallbackHadRun = false;

            var exceptions = new Dictionary<Type, Action>();
            exceptions.Add(typeof(NotImplementedException), () => notImplementedExceptionFallbackHadRun = true);
            exceptions.Add(typeof(ArithmeticException), () => arithmeticExceptionFallbackHadRun = true);

            FaultTolerance.TryFallback(exceptions, action, 2);

            Assert.False(notImplementedExceptionFallbackHadRun);
            Assert.True(arithmeticExceptionFallbackHadRun);
        }

        [Fact]
        public void Try_NonPositiveCount_ThrowsArgumentException()
        {
            Action action = () =>{ };

            Assert.Throws<ArgumentException>(() => FaultTolerance.Try<Exception>(action, -1));
        }

        [Fact]
        public void Try_NotAllTypesDerivedFromException_ThrowsArgumentException()
        {
            Action action = () => { };

            var exceptions = new List<Type> { typeof(int), typeof(Exception) };

            Assert.Throws<ArgumentException>(() => FaultTolerance.Try(exceptions, action, -1));
        }

        [Fact]
        public void TryFallback_NonPositiveCount_ThrowsArgumentException()
        {
            Action action = () => { };
            Action fallback = () => { };

            Assert.Throws<ArgumentException>(() => FaultTolerance.TryFallback<Exception>(action, -1, fallback));
        }

        [Fact]
        public void TryFallback_NotAllTypesDerivedFromException_ThrowsArgumentException()
        {
            Action action = () => { };
            Action fallback = () => { };

            var exceptionFallbacks = new Dictionary<Type, Action>();

            exceptionFallbacks.Add(typeof(int), fallback);
            exceptionFallbacks.Add(typeof(string), fallback);

            Assert.Throws<ArgumentException>(() => FaultTolerance.TryFallback(exceptionFallbacks, action, -1));
        }
    }
}
