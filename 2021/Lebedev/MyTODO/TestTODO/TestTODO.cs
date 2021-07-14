using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTODO
{
    [TestClass]
    public class TestTODO
    {
        [TestMethod]
        public void AddNullUnit()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();

            bool result = list.Add(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddEmptyUnit()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();

            bool result = list.Add("");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddExistingUncompletedUnit()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();

            bool result = list.Add("A");
            bool result2 = list.Add("A");

            Assert.IsTrue(result);
            Assert.AreEqual(list.Count, 1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void AddUnit()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();

            bool result = list.Add("A");
            bool result2 = list.Add("A");
            bool result3 = list.Add("B");

            Assert.IsTrue(result);
            Assert.IsFalse(result2);
            Assert.IsTrue(result3);
            Assert.AreEqual(list.Count, 2);
        }

        [TestMethod]
        public void ChangeState()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");
            list.Add("B");

            list.items[0].ChangeState(-1);
            list.items[1].ChangeState(1);

            Assert.AreEqual(list.items[0].state, -1);
            Assert.AreEqual(list.items[1].state, 1);
        }

        [TestMethod]
        public void ChangeStateDeny()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");
            list.Add("AA");
            list.Add("B");
            list.Add("BB");
            list.items[0].ChangeState(-1);
            list.items[1].ChangeState(-1);
            list.items[2].ChangeState(1);
            list.items[3].ChangeState(1);

            list.items[0].ChangeState(0);
            list.items[1].ChangeState(1);
            list.items[2].ChangeState(0);
            list.items[3].ChangeState(-1);

            Assert.AreEqual(list.items[0].state, -1);
            Assert.AreEqual(list.items[1].state, -1);
            Assert.AreEqual(list.items[2].state, 1);
            Assert.AreEqual(list.items[3].state, 1);
        }

        [TestMethod]
        public void ChangeName()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");

            list.items[0].ChangeName("B");

            Assert.AreEqual(list.items[0].name, "B");
        }

        [TestMethod]
        public void ChangeNameDeny()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");

            list.items[0].ChangeName("");

            Assert.AreEqual(list.items[0].name, "A");
        }

        [TestMethod]
        public void NameAvailable()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");

            bool res = list.NameAvailable("B");

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void NameNotAvailable()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");

            bool res = list.NameAvailable("A");

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void NameAvailableCauseDeleted()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");
            list.items[0].ChangeState(-1);

            bool res1 = list.NameAvailable("A");

            Assert.IsTrue(res1);
        }

        [TestMethod]
        public void NameAvailableCauseCompleted()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();
            list.Add("A");
            list.items[0].ChangeState(1);

            bool res1 = list.NameAvailable("A");

            Assert.IsTrue(res1);
        }

        [TestMethod]
        public void SaveAndLoad()
        {
            MyTODO.ListTODO list = new MyTODO.ListTODO();

            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.items[0].ChangeState(-1);
            list.items[2].ChangeState(1);
            var file = new System.IO.FileInfo("testsave.nottxt");

            
                list.SaveToFile(file);
                MyTODO.ListTODO list1 = new MyTODO.ListTODO(file);

                Assert.AreEqual(list1.Count, 3);
                Assert.AreEqual(list1.items[0].state, -1);
                Assert.AreEqual(list1.items[0].name, "A");
                Assert.AreEqual(list1.items[1].state, 0);
                Assert.AreEqual(list1.items[1].name, "B");
                Assert.AreEqual(list1.items[2].state, 1);
                Assert.AreEqual(list1.items[2].name, "C");

                if (file.Exists)
                    file.Delete();
        }
    }
}
