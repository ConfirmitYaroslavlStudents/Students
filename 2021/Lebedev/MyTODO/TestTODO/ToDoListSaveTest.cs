using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ToDoTest
{
    [TestClass]
    public class ToDoListSaveTest
    {
        [TestMethod]
        public void SaveAndLoad()
        {
            var file = new System.IO.FileInfo("testsave.nottxt");
            MyTODO.ToDoList list = new MyTODO.ToDoList(null);

            list.Add("A");
            list.Add("B");
            list.Add("C");
            list[0].Delete();
            list[2].Complete();
            try
            {
                MyTODO.ToDoListReducer.Save(file, list);
                MyTODO.ToDoList list1 = new MyTODO.ToDoList(file);

                Assert.AreEqual(3, list1.Count);
                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(list1[i].Name, list[i].Name);
                    Assert.AreEqual(list1[i].State, list[i].State);
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
