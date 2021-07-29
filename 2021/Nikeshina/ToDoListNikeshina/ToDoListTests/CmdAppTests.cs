using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class CmdAppTests
    {
        [TestMethod]
        public void MsgListIsEmpty()
        {
            var testLogger = new CmdTestLogger(new List<string>());
            var filehelper = new FileOperation(testLogger);

            filehelper.Load();

            Assert.AreEqual(testLogger.Messages.Count, 0);
        }

        [TestMethod]
        public void AddNewItem()
        {
            var testlogger = new CmdTestLogger(new List<string> { "add", "Купить арбуз" });
            var app = new CmdApp(testlogger);
            app.StringHandling();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> {  "Done! " });
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var testlogger = new CmdTestLogger(new List<string> {"add", "Купить арбуз","edit", "15" });
            var app = new CmdApp(testlogger);

            app.StringHandling();
            app.StringHandling();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Done! ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfString()
        {
            var testlogger = new CmdTestLogger(new List<string> {"add", "" });
            var app = new CmdApp(testlogger);

            app.StringHandling();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> {  "Incorrect data" });
        }


        [TestMethod]
        public void PrintToDoList()
        {
            var testlogger = new CmdTestLogger(new List<string> {"add", "Купить арбуз", "add", "Вымыть посуду", "list" });
            var app = new CmdApp(testlogger);

            app.StringHandling();
            app.StringHandling();

            var msgs = new List<string>();
            msgs.Add("Done! ");
            msgs.Add("Done! ");

            app.StringHandling();

            msgs.Add("1. Купить арбуз False");
            msgs.Add("2. Вымыть посуду False");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }

        [TestMethod]
        public void ListIsEmptyAfterDelete()
        {
            var testlogger = new CmdTestLogger(new List<string> {"add", "Купить арбуз","delete", "1" });
            var app = new CmdApp(testlogger);

            app.StringHandling();
            var msgs = new List<string>();
            msgs.Add("Done! ");

            app.StringHandling();
            msgs.Add("Done! ");



            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
        [TestMethod]
        public void ChangeStatus()
        {
            var testlogger = new CmdTestLogger(new List<string> { "add","Купить арбуз","change", "1" ,"list"});
            var app = new CmdApp(testlogger);

            app.StringHandling(); ;
            app.StringHandling();

            var msgs = new List<string>();
            msgs.Add("Done! ");
            msgs.Add("Done! ");

            app.StringHandling();

            msgs.Add("1. Купить арбуз True");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
        [TestMethod]
        public void EditDescription()
        {
            var testlogger = new CmdTestLogger(new List<string> {"add", "Купить арбуз","edit", "1", "Вымыть чайник","list" });
            var app = new CmdApp(testlogger);

            app.StringHandling();
            app.StringHandling();

            var msgs = new List<string>();
            msgs.Add("Done! ");
            msgs.Add("Done! ");

            app.StringHandling();

            msgs.Add("1. Вымыть чайник False");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }

        [TestMethod]
        public void CheckRollback()
        {
            var testlogger = new CmdTestLogger(new List<string> { "rollback", "1" });
            var app = new CmdApp(testlogger);

            app.StringHandling();
            var msgs = new List<string>();

            CollectionAssert.AreEqual(testlogger.Messages, msgs);
        }
    }
}
