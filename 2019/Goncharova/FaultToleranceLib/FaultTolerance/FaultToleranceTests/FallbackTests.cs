using FaultTolerance;
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
                () => Tolerance.Handle<InvalidCastException>().Fallback(null));
        }

        [Fact]
        public void FallbackAction_ActionDoesNotThrow_ShouldNotBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var tolerance = Tolerance.Handle<InvalidCastException>().Fallback(fallbackAction);
            tolerance.Execute(() => { });

            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_ActionThrowHandledException_ShouldBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var tolerance = Tolerance.Handle<InvalidCastException>().Fallback(fallbackAction);
            tolerance.Execute(() => { throw new InvalidCastException(); });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_ActionThrowOneOfTheHandledExceptions_ShouldBeRun()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var tolerance = Tolerance
                .Handle<InvalidCastException>()
                .Handle<DivideByZeroException>()
                .Fallback(fallbackAction);
            tolerance.Execute(() => { throw new InvalidCastException(); });

            Assert.True(fallbackActionIsRun);
        }

        [Fact]
        public void Action_ThrowUnhandledException_ShouldThrow()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }

            var tolerance = Tolerance.Handle<InvalidCastException>().Fallback(fallbackAction);
            void action() { throw new DivideByZeroException(); }

            Assert.Throws<DivideByZeroException>(() => tolerance.Execute(action));
            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void Action_ThrowOneOfTheUnhandledExceptions_ShouldThrow()
        {
            bool fallbackActionIsRun = false;
            void fallbackAction() { fallbackActionIsRun = true; }
            List<Exception> exceptions = new List<Exception>() {
                new InvalidCastException(), new InvalidOperationException() };

            var tolerance = Tolerance
                .Handle<InvalidCastException>()
                .Handle<InvalidOperationException>()
                .Fallback(fallbackAction);
            void action() { throw new DivideByZeroException(); }

            Assert.Throws<DivideByZeroException>(() => tolerance.Execute(action));
            Assert.False(fallbackActionIsRun);
        }

        [Fact]
        public void FallbackAction_IsRanAndThrowsUnhandledException_ShouldThrow()
        {
            void fallbackAction() { throw new DivideByZeroException(); }

            var tolerance = Tolerance.Handle<InvalidCastException>().Fallback(fallbackAction);
            void action() { throw new InvalidCastException(); }

            Assert.Throws<DivideByZeroException>(() => tolerance.Execute(action));
        }

    }
}
