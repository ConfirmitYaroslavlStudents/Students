using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToDoList;
using System.Collections.Generic;

namespace TestProjectForToDoList
{

    [TestClass]
    public class TestsForConvertNoteToSaveAndLoad
    {
        [TestMethod]
        public void ConvertLoadedCorrectStrings()
        {
            var lines = new string[]
            {
                "IFirst=ON",
                "ISecond=OFF"
            };

            var notes = new Note[]
            {
                new Note{Text ="IFirst",isCompletedFlag = true},
                new Note{Text = "ISecond",isCompletedFlag = false }
            };

           var result = new List<Note> ( ConvertNoteToSaveAndLoad.ConvertLinesAfterLoading(lines) );

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Text,notes[i].Text);
                Assert.AreEqual(result[i].isCompletedFlag, notes[i].isCompletedFlag);
            };
        }

        [TestMethod]
        public void ConvertLoadedIncorrectStrings()
        {
            var lines = new string[]
            {
                "=OFF=ON=on",
                "I Incorrect HA-HA"
            };

            var notes = new Note[]
            {
                new Note{Text ="=OFF=ON=on",isCompletedFlag = false},
                new Note{Text = "I Incorrect HA-HA",isCompletedFlag = false }
            };

            var result = new List<Note>(ConvertNoteToSaveAndLoad.ConvertLinesAfterLoading(lines));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Text, notes[i].Text);
                Assert.AreEqual(result[i].isCompletedFlag, notes[i].isCompletedFlag);
            };
        }

        [TestMethod]
        public void ConvertNotesToSave()
        {
            var notes = new Note[]
            {
                new Note{Text ="IFirst",isCompletedFlag = true},
                new Note{Text = "ISecond",isCompletedFlag = false }
            };

            var lines = new string[]
            {
                "IFirst=ON",
                "ISecond=OFF"
            };

            var result = new List<string>(ConvertNoteToSaveAndLoad.ConvertNoteToSave(notes));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i], lines[i]);
            };
        }
    }
}
