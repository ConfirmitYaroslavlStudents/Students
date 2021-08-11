using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;

namespace ToDoTest
{
    [TestClass]
    public class ToDoListSaveTest
    {
        [TestMethod]
        public void SaveAndLoad()
        {
            var file = new FileInfo("testsave.nottxt");
            var list = new ToDoList()
            {
                "A",
                "B",
                "C"
            };
            list[0].SetDeleted();
            list[2].SetCompleted();
            try
            {
                var restorer = new ToDoFileManager(file);
                restorer.Save(list);
                var list1 = new ToDoList(restorer.Read());

                Assert.AreEqual(3, list1.Count);
                for (var i = 0; i < 3; i++)
                {
                    Assert.AreEqual(list[i].Name, list1[i].Name);
                    Assert.AreEqual(list[i].Deleted, list1[i].Deleted);
                    Assert.AreEqual(list[i].Completed, list1[i].Completed);
                }
            }
            finally
            {
                if (file.Exists)
                    file.Delete();
            }
        }
    }
}
