using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;

namespace TestProjectForToDoList
{

    [TestClass]
    public class TestsForNote
    {
        [TestMethod]
        public void EqualNoteDuringConvertingToLine()
        {
            var noteDuring = new Note { Text = "I Work", isCompletedFlag = false };
            Assert.AreEqual(noteDuring.ToString(),"I Work");
        }

        [TestMethod]
        public void EqualNoteCompletedConvertingToLine()
        {
            var noteCompleted = new Note { Text = "I Completed!", isCompletedFlag = true };
            Assert.AreEqual(noteCompleted.ToString(), "I Completed! [X]");
        }
    }
}
