using FaultTolerance.Fallback;
using System;
using System.Collections.Generic;
using Xunit;

namespace FaultToleranceTests
{
    public class FallbackTests
    {
        [Fact]
        public void FallbackAction_IsNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FallbackStrategy(new InvalidCastException(), null));
        }

        [Fact]
        public void ExceptionParam_IsNull_ShouldTrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FallbackStrategy(exception: null, () => { }));
        }

        [Fact]
        public void ExceptionsListParam_IsNull_ShouldTrow()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FallbackStrategy(exceptions: null, () => { }));
        }

        [Fact]
        public void FallbackAction_ActionDoesNotThrow_ShouldNotBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var strategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            strategy.Execute(() => { });

            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_ActionThrowHandledException_ShouldBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var strategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            strategy.Execute(() => { throw new InvalidCastException(); });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_ActionThrowOneOfTheHandledExceptions_ShouldBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };

            var strategy = new FallbackStrategy(exceptions, fallbackAction);
            strategy.Execute(() => { throw new InvalidCastException(); });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void Action_ThrowUnhandledException_ShouldThrow()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var strategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            void action() { throw new DivideByZeroException(); }

            Assert.Throws<DivideByZeroException>(() => strategy.Execute(action));
            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void Action_ThrowOneOfTheUnhandledExceptions_ShouldThrow()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };

            var strategy = new FallbackStrategy(exceptions, fallbackAction);
            void action() { throw new DivideByZeroException(); }

            Assert.Throws<DivideByZeroException>(() => strategy.Execute(action));
            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_IsRanAndThrowsUnhandledException_ShouldThrow()
        {
            void fallbackAction() { throw new DivideByZeroException(); }

            var strategy = new FallbackStrategy(new InvalidCastException(), fallbackAction);
            void action() { throw new InvalidCastException(); }

            Assert.Throws<DivideByZeroException>(() => strategy.Execute(action));
        }

        [Fact]
        public void Action_IsFunc_ShouldThrow()
        {
            void fallbackAction() { }

            var strategy = new FallbackStrategy(new DivideByZeroException(), fallbackAction);
            int action() { return 5; }

            Assert.Throws<InvalidOperationException>(() => strategy.Execute(action));
        }

    }
}
