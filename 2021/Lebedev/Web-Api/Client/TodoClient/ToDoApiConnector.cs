using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using MyTODO;
using System.Text.Json;

namespace ToDoClient
{
    class ToDoApiConnector:IToDoConnector
    {
        private readonly string _url = "http://localhost:5000/todolist";

        public ToDoApiConnector(string url)
        {
            _url = url;
        }

        public ToDoApiConnector()
        {
        }

        public ToDoList FindAll(bool completed, bool deleted)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_url);
            webRequest.Method = "GET";
            using var webResponse = webRequest.GetResponse();
            using var responseStream = webResponse.GetResponseStream();
            using var reader = new StreamReader(responseStream);
            var answer = reader.ReadToEnd();
            var list = JsonSerializer.Deserialize<List<ToDoItem>>(answer);
            return (new ToDoList(list)).FindAll(completed, deleted);
        }

        public void Complete(int id)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_url);
            webRequest.Method = "PATCH";
            webRequest.ContentType = "application/json";
            var serializedData = JsonSerializer.Serialize(new ToDoItem(id){completed = true});
            var encodedData = Encoding.UTF8.GetBytes(serializedData);
            webRequest.ContentLength = encodedData.Length;
            using var requestStream = webRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);
            requestStream.Close();
            webRequest.GetResponse();
        }

        public void Delete(int id)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_url+ "/"+id);
            webRequest.Method = "DELETE";
            webRequest.GetResponse();
        }

        public void ChangeName(int id, string name)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_url);
            webRequest.Method = "PATCH";
            webRequest.ContentType = "application/json";
            var serializedData = JsonSerializer.Serialize(new ToDoItem(){id=id, name = name});
            var encodedData = Encoding.UTF8.GetBytes(serializedData);
            webRequest.ContentLength = encodedData.Length;
            using var requestStream = webRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);
            requestStream.Close();
            webRequest.GetResponse();
        }

        public void Add(string name)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(_url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json"; 
            var serializedData = JsonSerializer.Serialize(name);
            var encodedData = Encoding.UTF8.GetBytes(serializedData);
            webRequest.ContentLength = encodedData.Length;
            using var requestStream = webRequest.GetRequestStream();
            requestStream.Write(encodedData, 0, encodedData.Length);
            requestStream.Close();
            webRequest.GetResponse();
        }

        public ToDoItem GetItem(int id)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(_url + "/" + + id);
            webRequest.Method = "GET";
            using var webResponse = webRequest.GetResponse();
            using var responseStream = webResponse.GetResponseStream();
            using var reader = new StreamReader(responseStream);
            var answer = reader.ReadToEnd();
            var item = JsonSerializer.Deserialize<ToDoItem>(answer);
            return item;
        }
    }
}
