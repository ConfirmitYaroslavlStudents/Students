using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace ToDoListServerTests.IntegrationTests
{
    public static class RequestContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        public static StringContent GetPatchStringContent(object obj )
            => new(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json-patch+json");
    }
}
