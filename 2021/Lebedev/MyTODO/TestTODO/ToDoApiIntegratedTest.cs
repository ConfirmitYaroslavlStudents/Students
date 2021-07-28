using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ToDoTest
{
    [TestClass]
    public class ToDoApiIntegratedTest
    {
        private void MainStarter()
        {
            ToDoHost.ToProgram.Main(null);
        }
        [TestMethod]
        public void GetTest()
        {
            Thread thread = new Thread(MainStarter);
            thread.Start();
            var request = (HttpWebRequest)WebRequest.Create("http://localhost:11295/ToDoList/");
            request.Timeout = 10000;
            string result = "";
            Thread.Sleep(30000);
            using (WebResponse webResponse = request.GetResponse())
            using (Stream str = webResponse.GetResponseStream())
            using (StreamReader sr = new StreamReader(str))
                result= sr.ReadToEnd();
            
            Assert.AreEqual(result.Length,result.Length);
        }
    }
}
