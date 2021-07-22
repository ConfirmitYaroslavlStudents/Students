using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;

namespace TestProjectForToDoList
{

    [TestClass]
    public class UnitTestForNote
    {
        [TestMethod]
        public void EqualNoteDuringConvertingToLine()
        {
            var noteDuring = new Note { Text = "I Work", isCompleted = false };
            Assert.AreEqual(noteDuring.ToString(),"I Work");
        }

        [TestMethod]
        public void EqualNoteCompletedConvertingToLine()
        {
            var noteCompleted = new Note { Text = "I Completed!", isCompleted = true };
            Assert.AreEqual(noteCompleted.ToString(), "X I Completed!");
        }
    }
}
