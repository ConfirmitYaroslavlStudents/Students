using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasker.Core;

namespace Tasker.Tests
{
    [TestClass]
    public class ExecutionConditionTest
    {
        [TestMethod]
        public void ListOfPreviousStatesIsEmpty()
        {
            List<State> emptyList = new List<State>();

            Assert.AreEqual(true,
                ExecutionCondition.CanExecute(ExecutionCondition.Always, emptyList));
            Assert.AreEqual(true, 
                ExecutionCondition.CanExecute(ExecutionCondition.IfPreviousIsSuccessful, emptyList));
            Assert.AreEqual(true,
                ExecutionCondition.CanExecute(ExecutionCondition.IfSuccessfulBy(100), emptyList));
        }

        [TestMethod]
        public void AlwaysCondition()
        {
            List<State> previousStates = new List<State> {State.NotRunning, State.Failed, State.Failed};

            Assert.AreEqual(true,
                ExecutionCondition.CanExecute(ExecutionCondition.Always, previousStates));
        }

        [TestMethod]
        public void IfPreviousIsSuccessfulCondition()
        {
            List<State> successfulPreviousStates = new List<State> { State.Failed, State.Successful };
            List<State> failedPreviousStates = new List<State> { State.Successful, State.Failed };

            Assert.AreEqual(true,
                ExecutionCondition.CanExecute(ExecutionCondition.IfPreviousIsSuccessful, successfulPreviousStates));
            Assert.AreEqual(false,
                ExecutionCondition.CanExecute(ExecutionCondition.IfPreviousIsSuccessful, failedPreviousStates));
        }

        [TestMethod]
        public void SuccessfulStateByIndex()
        {
            List<State> previousStates = new List<State> { State.Failed, State.Successful, State.NotRunning, State.Failed, State.Failed};

            Assert.AreEqual(true,
                ExecutionCondition.CanExecute(ExecutionCondition.IfSuccessfulBy(1), previousStates));
            Assert.AreEqual(false,
                ExecutionCondition.CanExecute(ExecutionCondition.IfSuccessfulBy(2), previousStates));
            Assert.AreEqual(false,
                ExecutionCondition.CanExecute(ExecutionCondition.IfSuccessfulBy(0), previousStates));
        }

        [TestMethod]
        public void SuccessfulStateIndexOutRange()
        {
            List<State> previousStates = new List<State> {State.Successful};

            Assert.AreEqual(false,
                ExecutionCondition.CanExecute(ExecutionCondition.IfSuccessfulBy(17), previousStates));
        }
    }
}