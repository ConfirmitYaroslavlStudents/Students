using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using MyTODO;

namespace TodoServerTest
{
    [TestClass]
    public class ToDoApiIntegratedTest
    {
        private class ToDoApiTestConnector
        {
            private const string Url = "http://localhost:5000/todolist";

            public ToDoList FindAll(bool completed = true, bool deleted = true)
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/?test=true");
                webRequest.Method = "GET";
                using var webResponse = webRequest.GetResponse();
                using var responseStream = webResponse.GetResponseStream();
                using var reader = new StreamReader(responseStream);
                var answer = reader.ReadToEnd();
                var list = JsonConvert.DeserializeObject<List<ToDoItem>>(answer);
                return (new ToDoList(list)).FindAll(completed, deleted);
            }

            public void Delete(int id)
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/" + id + "/?test=true");
                webRequest.Method = "DELETE";
                webRequest.GetResponse();
            }

            public void ChangeName(int id, string name)
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/?test=true");
                webRequest.Method = "PATCH";
                webRequest.ContentType = "application/json";
                var serializedData = JsonConvert.SerializeObject(new ToDoItem() { Id = id, Name = name });
                var encodedData = Encoding.UTF8.GetBytes(serializedData);
                webRequest.ContentLength = encodedData.Length;
                using var requestStream = webRequest.GetRequestStream();
                requestStream.Write(encodedData, 0, encodedData.Length);
                requestStream.Close();
                webRequest.GetResponse();
            }

            public void Add(string name)
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/?test=true");
                webRequest.Method = "POST";
                webRequest.ContentType = "application/json";
                var serializedData = JsonConvert.SerializeObject(new ToDoItem() { Id = 0, Name = name, Completed = false, Deleted = false });
                var encodedData = Encoding.UTF8.GetBytes(serializedData);
                webRequest.ContentLength = encodedData.Length;
                using var requestStream = webRequest.GetRequestStream();
                requestStream.Write(encodedData, 0, encodedData.Length);
                requestStream.Close();
                webRequest.GetResponse();
            }

            public ToDoItem GetItem(int id)
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/" + id + "/?test=true");
                webRequest.Method = "GET";
                using var webResponse = webRequest.GetResponse();
                using var responseStream = webResponse.GetResponseStream();
                using var reader = new StreamReader(responseStream);
                var answer = reader.ReadToEnd();
                var item = JsonConvert.DeserializeObject<ToDoItem>(answer);
                return item;
            }

            public void DeleteList()
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(Url + "/deletelist/?test=true");
                webRequest.Method = "DELETE";
                webRequest.GetResponse();
            }
        }

        private ToDoApiTestConnector _apiTestConnector;

        [TestCleanup]
        public void CleanupCode()
        {
            _apiTestConnector.DeleteList();
        }

        [TestInitialize]
        public void Initializer()
        {
            _apiTestConnector = new ToDoApiTestConnector();
        }

        [TestMethod]
        public void GetTest()
        {
            _apiTestConnector.Add("test1");

            var toDoList = _apiTestConnector.FindAll();

            Assert.AreEqual(1, toDoList.Count);
        }

        [TestMethod]
        public void GetEmptyListTest()
        {
            var toDoList = _apiTestConnector.FindAll();

            Assert.AreEqual(0, toDoList.Count);
        }

        [TestMethod]
        public void PostItemTest()
        {
            var toDoListBefore = _apiTestConnector.FindAll();

            _apiTestConnector.Add("test1");
            var toDoListAfter = _apiTestConnector.FindAll();

            Assert.AreEqual(toDoListBefore.Count + 1, toDoListAfter.Count);
            Assert.AreEqual(toDoListAfter[0].Name, "test1");
        }

        [TestMethod]
        public void PostItemWithEmptyNameTest()
        {
            _apiTestConnector.FindAll();

            Assert.ThrowsException<WebException>(() => _apiTestConnector.Add(""));
        }

        [TestMethod]
        public void GetItemTest()
        {
            _apiTestConnector.Add("test1");

            var toDoItem = _apiTestConnector.GetItem(0);

            Assert.AreEqual(toDoItem.Name, "test1");
        }

        [TestMethod]
        public void GetItemLessThanZeroIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.GetItem(-1));
        }

        [TestMethod]
        public void GetItemOutOfBoundsIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.GetItem(1));
        }

        [TestMethod]
        public void GetItemFromEmptyListTest()
        {
            Assert.ThrowsException<WebException>(() => _apiTestConnector.GetItem(0));
        }

        [TestMethod]
        public void PatchNameTest()
        {
            _apiTestConnector.Add("test1");

            _apiTestConnector.ChangeName(0, "test2");
            var toDoItem = _apiTestConnector.GetItem(0);

            Assert.AreEqual(toDoItem.Name, "test2");
        }

        [TestMethod]
        public void PatchNameLessThanZeroIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.ChangeName(-1, "test1"));
        }

        [TestMethod]
        public void PatchNameOutOfBoundsIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.ChangeName(1, "test1"));
        }

        [TestMethod]
        public void PatchNameInEmptyListTest()
        {
            Assert.ThrowsException<WebException>(() => _apiTestConnector.ChangeName(0, "test1"));
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            _apiTestConnector.Add("test1");

            _apiTestConnector.Delete(0);
            var toDoItem = _apiTestConnector.GetItem(0);

            Assert.IsTrue((bool)toDoItem.Deleted);
        }

        [TestMethod]
        public void DeleteItemLessThanZeroIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.Delete(-1));
        }

        [TestMethod]
        public void DeleteItemOutOfBoundsIdTest()
        {
            _apiTestConnector.Add("test1");

            Assert.ThrowsException<WebException>(() => _apiTestConnector.Delete(1));
        }

        [TestMethod]
        public void DeleteItemFromEmptyListTest()
        {
            Assert.ThrowsException<WebException>(() => _apiTestConnector.Delete(0));
        }
    }
}
