using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoLibrary;
using ToDoLibrary.SaveAndLoad;

namespace TestProjectForToDoLibrary
{
    [TestClass]
    public class TestsForToDoApp
    {
        [TestMethod]
        public void LoadWithFileNotFound()
        {
            var logger = new FakeLogger();
            var toDoApp = new ToDoApp(logger, new FakeSaver(), new FileLoader("",logger));

            Assert.AreEqual("Saved data was not found. New list created.", logger.Exception.Message);
        }
    }
}