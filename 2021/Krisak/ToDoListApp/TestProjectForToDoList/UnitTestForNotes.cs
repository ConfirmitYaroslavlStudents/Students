using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;
using System;

namespace TestProjectForToDoList
{

    [TestClass]
    public class UnitTestForNote
    {
        private Note _note; 

        [TestMethod]
        public void CreateItemUsingBaseConstructor()
        {
            _note = new Note();
            Assert.AreEqual(_note.Text, "");
        }

        [TestMethod]
        public void CreateItemUsingConstructorWithParameter()
        {
            var line = DateTime.Now.ToString();
            _note = new Note(line);
            Assert.AreEqual(_note.Text, line);
        }

        [TestMethod]
        public void ChangeText()
        {
            var line = DateTime.Now.ToString();
            _note = new Note();
            _note.Text = line;
            Assert.AreEqual(_note.Text, line);
        }
    }

    [TestClass]
    public class UnitTestForCompletedNote
    {
        private NoteCompleted _note;

        [TestMethod]
        public void CreateItemUsingBaseConstructor()
        {
            _note = new NoteCompleted();
            Assert.AreEqual(_note.Text, "");
        }

        [TestMethod]
        public void CreateItemUsingConstructorWithParameter()
        {
            var line = DateTime.Now.ToString();
            _note = new NoteCompleted(line);
            Assert.AreEqual(_note.Text, line);
        }

        [TestMethod]
        public void ChangeText()
        {
            var line = DateTime.Now.ToString();
            _note = new NoteCompleted();
            _note.Text = line;
            Assert.AreEqual(_note.Text, line);
        }
    }
}
