using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoLibrary;
using ToDoLibrary.SaveAndLoad;

namespace TestProjectForToDoLibrary
{

    [TestClass]
    public class TestsForConvertTaskToSaveAndLoad
    {
        [TestMethod]
        public void ConvertLoadedCorrectStrings()
        {
            var lines = new []
            {
                "IFirst=ToDo",
                "ISecond=InProgress",
                "IThird=Done"
            };

            var tasks = new Task[]
            {
                new Task{Text ="IFirst",Status = StatusTask.ToDo},
                new Task{Text = "ISecond",Status = StatusTask.IsProgress},
                new Task{Text = "IThird",Status = StatusTask.Done}
            };

            var result = new List<Task>(TasksToSaveAndLoadConverter.ConvertTasksAfterLoading(lines));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(tasks[i].Text, result[i].Text);
                Assert.AreEqual(tasks[i].Status, result[i].Status);
            };
        }

        [TestMethod]
        public void ConvertLoadedIncorrectStrings()
        {
            var lines = new []
            {
                "=Done=ToDo=Inprogress",
                "I Incorrect HA-HA"
            };

            var notes = new Task[]
            {
                new Task{Text ="=Done=ToDo=Inprogress",Status = StatusTask.ToDo},
                new Task{Text = "I Incorrect HA-HA",Status = StatusTask.ToDo}
            };

            var result = new List<Task>(TasksToSaveAndLoadConverter.ConvertTasksAfterLoading(lines));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(notes[i].Text, result[i].Text);
                Assert.AreEqual(notes[i].Status, result[i].Status);
            };
        }

        [TestMethod]
        public void ConvertTaskToSave()
        {
            var tasks = new []
            {
                new Task{Text ="IFirst",Status = StatusTask.ToDo},
                new Task{Text = "ISecond",Status = StatusTask.IsProgress},
                new Task{Text = "IThird",Status = StatusTask.Done}
            };

            var lines = new []
            {
                "IFirst=ToDo",
                "ISecond=InProgress",
                "IThird=Done"
            };

            var result = new List<string>(TasksToSaveAndLoadConverter.ConvertTasksToSave(tasks));

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i], lines[i]);
            };
        }
    }
}
