using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class ConsoleTest
    {
        List<Task> list = new List<Task>();

        [TestMethod]
        public void AddNewItem()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple" }),list);

            app.AddNewTask();
            var msg = new List<string> { "Description: ", "Done! " };
            CollectionAssert.AreEqual(logger.Messages, msg);
            CollectionAssert.AreEqual(app.GetListOfTask(), new List<Task> { new Task("buy apple", StatusOfTask.Todo) });
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple","15" }),list);

            app.AddNewTask();
            app.EditDescription();

            CollectionAssert.AreEqual(logger.Messages, new List<string> {"Description: ", "Done! ",
                "Number of the note: ", "Incorrect data" });
        }

        [TestMethod]
        public void WrongFormtOfString()
        {
             var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "" }),list);

            app.AddNewTask();

            CollectionAssert.AreEqual(logger.Messages, new List<string> { "Description: ", "Incorrect data" });
        }

        [TestMethod]
        public void LongDescriptionString()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "hvghcfcgh gfc ddfzf gfhggfc d xghv ghgd xszdhgvghdgcxszdggjh es rfxszd     tygj  ft hjghgf " }),list);

            app.AddNewTask();

            CollectionAssert.AreEqual(logger.Messages, new List<string> { "Description: ",  "Incorrect data" });
        }

        [TestMethod]
        public void PrintToDoList()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple", "make tea","1" }),list);
            var checkinglist = new List<Task> { new Task("buy apple", StatusOfTask.InProgress), new Task("make tea", StatusOfTask.Todo) };

            app.AddNewTask();
            app.AddNewTask();
            app.ChangeStatus();

            var msgs = new List<string>();
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");


            app.Print();

            msgs.Add("1. buy apple InProgress");
            msgs.Add("2. make tea Todo");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(checkinglist, app.GetListOfTask());
        }

        [TestMethod]
        public void ListIsEmptyAfterDelete()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple", "1" }),list);

            app.AddNewTask();
            app.Delete();

            var msgs = new List<string>();
            msgs.Add("Description: ");
            msgs.Add("Done! ");
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");

            app.Print();
            msgs.Add("List is empty(");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(new List<Task>(), app.GetListOfTask());
        }

        [TestMethod]
        public void EditDescription()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple",  "1" , "buy pineapple"}),list);

            var msgs = new List<string>();

            app.AddNewTask();
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.EditDescription();
            msgs.Add("Number of the note: ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(new List<Task> { new Task("buy pineapple", StatusOfTask.Todo) }, app.GetListOfTask());
        }

        

        [TestMethod]
        public void CheckRollback()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple", "1", "1","buy pineapple","2" }),list);

            var msgs = new List<string>();

            app.AddNewTask();
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.ChangeStatus();
            msgs.Add("Number of the note: ");
            msgs.Add("Done! ");

            app.EditDescription();
            msgs.Add("Number of the note: ");
            msgs.Add("Description: ");
            msgs.Add("Done! ");

            app.Rollback();
            msgs.Add("Number of commands: ");
            msgs.Add("Done! ");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(new List<Task> { new Task("buy apple", StatusOfTask.Todo) },app.GetListOfTask());
        }

        [TestMethod]
        public void CheckDescriptionLength()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[] { "buy apple yrg   gv yghgb jhb ujhjn jh ghg  dfx cfjghv hgykh gkhlgkbgj hg cgf " +
                "cfgcjm kgytfhfgvytftcd gvt  tf yvgvYTTCHVky2" }),list);

            var msgs = new List<string>();

            app.AddNewTask();
            msgs.Add("Description: ");
            msgs.Add("Incorrect data");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(new List<Task>(), app.GetListOfTask());
        }
        
        [TestMethod]
        public void AddFourTasksInProgress()
        {
            var logger = new TestLogger();
            var app = new ConsoleApp(logger, new CmdInputDataStorage(new string[]
            { "buy apple", "wash dishes", "do tasks", "buy pineapple","1" ,"2","4","3" }),list);

            for(int i=0;i<4;i++)
               app.AddNewTask();
            for (int i = 0; i < 4; i++)
                app.ChangeStatus();

            var checkingList = new List<Task> { new Task("buy apple", StatusOfTask.InProgress), new Task("wash dishes", StatusOfTask.InProgress),
                                                new Task("do tasks", StatusOfTask.Todo), new Task("buy pineapple", StatusOfTask.InProgress) };

            CollectionAssert.AreEqual(checkingList, app.GetListOfTask());
        }
    }
}
