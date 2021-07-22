using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MsgListIsEmpty()
        {
            var testLogger = new TestLogger();
            var filehelper = new FileOperation(testLogger);

            filehelper.Read();

            Assert.AreEqual(testLogger.Messages[0], "List is empty(");
        }

        [TestMethod]
        public void WrongFormtInputData()
        {
            var testlogger = new TestLogger(new List<string> { "������ �����" });
            var app = new ConsoleApp(testlogger);

            app.Add();

            Assert.AreEqual(testlogger.Messages[0], "Description: ");
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var testlogger = new TestLogger(new List<string> { "������ �����", "15"});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Description: ",
                "Number of the note: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfString()
        {
            var testlogger = new TestLogger(new List<string> { "" });
            var app = new ConsoleApp(testlogger);

            app.Add();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Description: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfEditString()
        {
            var testlogger = new TestLogger(new List<string> { "������ �����", "1" ,""});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Description: ", "Number of the note: ",
                "Description: ", "Incorrect data" });
        }

        [TestMethod]
        public void PrintToDoList()
        {
            var testlogger = new TestLogger(new List<string> { "������ �����", "������ ������", "������ �������","3","2","������ ������" });
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Add();
            app.Add();
            app.ChangeStatus();
            app.Edit();

            var msgs = new List<string>();
            msgs.Add("Description: ");
            msgs.Add("Description: ");
            msgs.Add("Description: ");
            msgs.Add("Number of the note: ");
            msgs.Add("Number of the note: ");
            msgs.Add("Description: ");

            app.Print();

            msgs.Add("1. ������ ����� False");
            msgs.Add("2. ������ ������ False");
            msgs.Add("3. ������ ������� True");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }

        [TestMethod]
        public void ListIsEmptyAfterDelete()
        {
            var testlogger = new TestLogger(new List<string> { "������ �����", "������ ������",
                "������ �������", "3", "2", "������ ������","1","1","1" });
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Add();
            app.Add();
            app.ChangeStatus();
            app.Edit();

            var msgs = new List<string>();
            msgs.Add("Description: ");
            msgs.Add("Description: ");
            msgs.Add("Description: ");
            msgs.Add("Number of the note: ");
            msgs.Add("Number of the note: ");
            msgs.Add("Description: ");

            app.Print();

            msgs.Add("1. ������ ����� False");
            msgs.Add("2. ������ ������ False");
            msgs.Add("3. ������ ������� True");

            app.Delete();
            app.Delete();
            app.Delete();

            msgs.Add("Number of the note: ");
            msgs.Add("Number of the note: ");
            msgs.Add("Number of the note: ");

            app.Print();

            msgs.Add("List is empty(");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
    }
}
