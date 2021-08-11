using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ToDoClient;
using System;

namespace ClientTests
{
    public class FakeApiClient : IApiClient
    {
        public string BaseApiServiceUrl { get; }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return new HttpResponseMessage {StatusCode = HttpStatusCode.OK};
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Headers = { Location = new Uri("https://localhost:44314/todo/1") } };
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
