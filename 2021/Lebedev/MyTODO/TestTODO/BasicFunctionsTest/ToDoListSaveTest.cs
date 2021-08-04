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
            list[0].Delete();
            list[2].Complete();
            try
            {
                var restorer = new ToDoFileManager(file);
                restorer.Save(list);
                var list1 = new ToDoList(restorer.Read());

                Assert.AreEqual(3, list1.Count);
                for (var i = 0; i < 3; i++)
                {
                    Assert.AreEqual(list[i].name, list1[i].name);
                    Assert.AreEqual(list[i].deleted, list1[i].deleted);
                    Assert.AreEqual(list[i].completed, list1[i].completed);
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
