using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class ConsoleAppTest
    {
        [TestMethod]
        public void MsgListIsEmpty()
        {
            var testLogger = new TestLogger();
            var filehelper = new FileOperation(testLogger);

            filehelper.Load();

            Assert.AreEqual(testLogger.Messages[0], "List is empty(");
        }

        [TestMethod]
        public void AddNewItem()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз" });
            var app = new ConsoleApp(testlogger);
            app.Add();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "List is empty(", "Description: ", "Done! " });
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "15"});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> {"List is empty(", "Description: ", "Done! ",
                "Number of the note: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfString()
        {
            var testlogger = new TestLogger(new List<string> { "" });
            var app = new ConsoleApp(testlogger);

            app.Add();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "List is empty(", "Description: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfEditString()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "1" ,""});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "List is empty(",  "Description: ", "Done! ",
                "Number of the note: ", "Description: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfStringNumber()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "2"});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "List is empty(",  "Description: ","Done! ",
                "Number of the note: ", "Incorrect data" });
        }

        [TestMethod]
        public void PrintToDoList()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "Вымыть посуду"});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Add();

            var msgs = new List<string>();
            msgs.Add("List is empty(");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.Print();

            msgs.Add("1. Купить арбуз False");
            msgs.Add("2. Вымыть посуду False");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }

        [TestMethod]
        public void ListIsEmptyAfterDelete()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "Вымыть посуду",  "1","1" });
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Add();

            var msgs = new List<string>();
            msgs.Add("List is empty(");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.Delete();
            app.Delete();

            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");

            app.Print();

            msgs.Add("List is empty(");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
        [TestMethod]
        public void ChangeStatus()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "1" });
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.ChangeStatus();

            var msgs = new List<string>();
            msgs.Add("List is empty(");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");

            app.Print();

            msgs.Add("1. Купить арбуз True");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
        [TestMethod]
        public void EditDescription()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "1", "Вымыть чайник" });
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Edit();

            var msgs = new List<string>();
            msgs.Add("List is empty(");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Number of the note: ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.Print();

            msgs.Add("1. Вымыть чайник False");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
    
        [TestMethod]
        public void CheckRollback()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "Вымыть посуду", "1", "2"});
            var app = new ConsoleApp(testlogger);

            app.Add();
            app.Add();

            var msgs = new List<string>();
            msgs.Add("List is empty(");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.ChangeStatus();
            app.Rollback();
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");
            msgs.Add("Number of commands: ");
            msgs.Add("Done! ");
            app.Print();
            msgs.Add("1. Купить арбуз False");
            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
    }
}
