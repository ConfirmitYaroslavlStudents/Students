﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoListNikeshina;

namespace ToDoListTests
{
    [TestClass]
    public class CmdAppTests
    {
        [TestMethod]
        public void AddNewItem()
        {
            var logger = new TestLogger();
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "buy apple" }));

            app.AddNewTask();
            var msg = new List<string> {"Done! " };
            CollectionAssert.AreEqual(logger.Messages, msg);
            CollectionAssert.AreEqual(app.GetListOfTask(), new List<Task> { new Task("buy apple", 0) });
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var logger = new TestLogger();
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "buy apple", "15" }));

            app.AddNewTask();
            app.EditDescription();

            CollectionAssert.AreEqual(logger.Messages, new List<string> { "Done! ", "Incorrect data" });
        }

        [TestMethod]
        public void LongDescriptionString()
        {
            var logger = new TestLogger();
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "hvghcfcgh gfc ddfzf gfhggfc d xghv ghgd xszdhgvghdgcxszdggjh es rfxszd     tygj  ft hjghgf " }));

            app.AddNewTask();

            CollectionAssert.AreEqual(logger.Messages, new List<string> {"Incorrect data" });
        }

        [TestMethod]
        public void PrintToDoList()
        {
            var logger = new TestLogger();
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "buy apple", "make tea", "1" }));
            var checkinglist = new List<Task> { new Task("buy apple", 1), new Task("make tea", 0) };

            app.AddNewTask();
            app.AddNewTask();
            app.ChangeStatus();

            var msgs = new List<string>();
            msgs.Add("Done! ");
            msgs.Add("Done! ");
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
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "buy apple", "1" }));

            app.AddNewTask();
            app.Delete();

            var msgs = new List<string>();
            msgs.Add("Done! ");
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
            var app = new CmdApp(logger, new CmdGetterInput(new string[] { "buy apple", "1", "buy pineapple" }));

            var msgs = new List<string>();

            app.AddNewTask();
            msgs.Add("Done! ");

            app.EditDescription();
            msgs.Add("Done! ");

            CollectionAssert.AreEqual(logger.Messages, msgs);
            CollectionAssert.AreEqual(new List<Task> { new Task("buy pineapple", 0) }, app.GetListOfTask());
        }


        [TestMethod]
        public void AddFourTasksInProgress()
        {
            var logger = new TestLogger();
            var app = new CmdApp(logger, new CmdGetterInput(new string[]
            { "buy apple", "wash dishes", "do tasks", "buy pineapple","1" ,"2","4","3" }));

            for (int i = 0; i < 4; i++)
                app.AddNewTask();
            for (int i = 0; i < 4; i++)
                app.ChangeStatus();

            var checkingList = new List<Task> { new Task("buy apple", 1), new Task("wash dishes", 1),
                                                new Task("do tasks", 0), new Task("buy pineapple", 1) };

            CollectionAssert.AreEqual(checkingList, app.GetListOfTask());
        }
    }
}
