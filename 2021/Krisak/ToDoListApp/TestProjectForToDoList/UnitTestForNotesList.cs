using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList;
using System;

namespace TestProjectForToDoList
{
    [TestClass]
    public class UnitTestForNotesList
    {
        private NotesList _list;

        [TestMethod]
        public void EmptyListAfterCreation()
        {
            _list = new NotesList();

            Assert.AreEqual(_list.Count, 0);
        }

        [TestMethod]
        public void CountListAfterAdding()
        {
            _list = new NotesList();
            var n = 5;
            for (var i = 0; i < n; i++)
                _list.Add(new Note(i.ToString()));

            Assert.AreEqual(_list.Count, 5);
        }

        [TestMethod]
        public void ItemsInListAfterRemove()
        {
            _list = new NotesList();

            _list.Add(new Note("0"));
            _list.Add(new NoteCompleted("1"));
            _list.Add(new NoteCompleted("2"));
            _list.Add(new Note("3"));

            _list.Remove(2);
            _list.Remove(1);

            Assert.AreEqual(_list[0].Text, "0");
            Assert.AreEqual(_list[1].Text, "3");
        }

        [TestMethod]
        public void ChangingItemsUsingChangeText()
        {
            _list = new NotesList();

            _list.Add(new Note("0"));
            _list.Add(new NoteCompleted("1"));

            _list.ChangeText("1", 0);
            _list.ChangeText("0", 1);

            Assert.AreEqual(_list[0].Text, "1");
            Assert.AreEqual(_list[1].Text, "0");
        }

        [TestMethod]
        public void ChangingItemsUsingChangeStatus()
        {
            _list = new NotesList();

            _list.Add(new Note("0"));
            var a = new NoteCompleted("0");

            _list.ChangeStatus(new NoteCompleted(), 0);

            Assert.AreEqual(_list[0].ToString(),a.ToString());
        }

        [TestMethod]
        public void EqualityOutputUsingOutputNotes()
        {
            _list = new NotesList();

            var a = new Note("0");
            var b = new NoteCompleted("1");

            _list.Add(a);
            _list.Add(b);

            var array1 = new string[]
            {
                a.ToString(),b.ToString()
            };
            var array2 = _list.OutputNotes();

            for (var i = 0; i < array1.Length; i++)
                Assert.AreEqual(array1[i], array2[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ExceptionAfterRequestingIndexGreaterCount()
        {
            _list = new NotesList();
            _list.Add(new Note());
            var a = _list[2];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionChangingAtNegativeIndex()
        {
            _list = new NotesList();
            _list.Add(new Note());
            _list.ChangeText("", -1);
        }
    }
}
