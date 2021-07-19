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
            var filehelper = new WorkWithFile(testLogger);

            filehelper.Read();

            Assert.AreEqual(testLogger.Messages[0], "Лист пуст");
        }

        [TestMethod]
        public void WrongFormtInputData()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз" });
            var app = new Application(testlogger);

            app.Add();
            Assert.AreEqual(testlogger.Messages[0], "Введите описание задачи: ");
        }

        [TestMethod]
        public void WrongFormtOfEditNumber()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "15"});
            var app = new Application(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Введите описание задачи: ", "Введите порядковый номер задания: ", "Введенные данные некорректны" });
        }

        [TestMethod]
        public void WrongFormtOfString()
        {
            var testlogger = new TestLogger(new List<string> { "" });
            var app = new Application(testlogger);

            app.Add();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Введите описание задачи: ", "Введенные данные некорректны" });
        }

        [TestMethod]
        public void WrongFormtOfEditString()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "1" ,""});
            var app = new Application(testlogger);

            app.Add();
            app.Edit();

            CollectionAssert.AreEqual(testlogger.Messages, new List<string> { "Введите описание задачи: ", "Введите порядковый номер задания: ", "Введите описание задачи: ", "Введенные данные некорректны" });
        }

        [TestMethod]
        public void PrintToDoList()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "Вымыть посуду", "Купить гантели","3","2","Вымыть чайник" });
            var app = new Application(testlogger);

            app.Add();
            app.Add();
            app.Add();
            app.ChangeStatus();
            app.Edit();

            var msgs = new List<string>();
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите описание задачи: ");

            app.Print();

            msgs.Add("1. Купить арбуз False");
            msgs.Add("2. Вымыть чайник False");
            msgs.Add("3. Купить гантели True");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);

        }

        [TestMethod]
        public void ListIsEmptyAfterDelete()
        {
            var testlogger = new TestLogger(new List<string> { "Купить арбуз", "Вымыть посуду",
                "Купить гантели", "3", "2", "Вымыть чайник","1","1","1" });
            var app = new Application(testlogger);

            app.Add();
            app.Add();
            app.Add();
            app.ChangeStatus();
            app.Edit();

            var msgs = new List<string>();
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите описание задачи: ");
            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите описание задачи: ");

            app.Print();

            msgs.Add("1. Купить арбуз False");
            msgs.Add("2. Вымыть чайник False");
            msgs.Add("3. Купить гантели True");

            app.Delete();
            app.Delete();
            app.Delete();

            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите порядковый номер задания: ");
            msgs.Add("Введите порядковый номер задания: ");

            app.Print();

            msgs.Add("Лист пуст");

            CollectionAssert.AreEqual(testlogger.Messages, msgs);

        }
    }
}
