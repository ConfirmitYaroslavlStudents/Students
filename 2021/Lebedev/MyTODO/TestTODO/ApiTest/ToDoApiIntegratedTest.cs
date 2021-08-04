using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTODO;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using ToDoHost;

namespace ToDoTest
{
    [TestClass]
    public class ToDoApiIntegratedTest
    {
        private static string file = "TODOsave.txt";
        [TestMethod]
        public void GetTest()
        {
            if (File.Exists(file))
                File.Delete(file);
            var webHost = new WebApplicationFactory<ToStartup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
            });
            var client = webHost.CreateClient();

            var result = client.GetAsync("/todolist");
            result.Wait();

            Assert.IsTrue(result.Result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostItemTest()
        {
            if (File.Exists(file))
                File.Delete(file);
            var webHost = new WebApplicationFactory<ToStartup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
            });
            var client = webHost.CreateClient();

            client.PostAsync("/todolist", new StringContent(@"""check""", Encoding.UTF8, "application/json")).Wait();
            var manager = new ToDoFileManager(new FileInfo(file));

            Assert.AreEqual(new ToDoItem(0, "check").ToString(), manager.Read().ElementAt(0).ToString());
        }

        [TestMethod]
        public void GetItemTest()
        {
            if (File.Exists(file))
                File.Delete(file);
            var webHost = new WebApplicationFactory<ToStartup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
            });
            var client = webHost.CreateClient();

            client.PostAsync("/todolist", new StringContent(@"""check""", Encoding.UTF8, "application/json")).Wait();
            var jsonitem = client.GetStringAsync("/todolist/0");
            jsonitem.Wait();

            Assert.AreEqual(@"{""id"":0,""name"":""check"",""completed"":false,""deleted"":false}", jsonitem.Result);
        }

        [TestMethod]
        public void PatchNameTest()
        {
            if (File.Exists(file))
                File.Delete(file);
            var webHost = new WebApplicationFactory<ToStartup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
            });
            var client = webHost.CreateClient();
            client.PostAsync("/todolist", new StringContent(@"""check""", Encoding.UTF8, "application/json")).Wait();
            var result = client.PatchAsync("/todolist", new StringContent(@"{""id"":0,""name"":""baba"",""completed"":false,""deleted"":false}", Encoding.UTF8, "application/json"));
            result.Wait();
            var jsonitem = client.GetStringAsync("/todolist/0");
            jsonitem.Wait();

            var ka = result.Result.Content.ReadAsStringAsync();
            ka.Wait();

            Assert.AreEqual(@"{""id"":0,""name"":""baba"",""completed"":false,""deleted"":false}", jsonitem.Result);
            Assert.AreEqual("Patch item 0 completed", ka.Result);
            Assert.IsTrue(result.Result.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            if (File.Exists(file))
                File.Delete(file);
            var webHost = new WebApplicationFactory<ToStartup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
            });
            var client = webHost.CreateClient();
            client.PostAsync("/todolist", new StringContent(@"""check""", Encoding.UTF8, "application/json")).Wait();
            var result = client.DeleteAsync("/todolist/0");
            result.Wait();
            var jsonitem = client.GetStringAsync("/todolist/0");
            jsonitem.Wait();

            var ka = result.Result.Content.ReadAsStringAsync();
            ka.Wait();

            Assert.AreEqual(@"{""id"":0,""name"":""check"",""completed"":false,""deleted"":true}", jsonitem.Result);
            Assert.AreEqual("Delete Completed", ka.Result);
            Assert.IsTrue(result.Result.IsSuccessStatusCode);
        }
    }
}
