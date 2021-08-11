using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoClient
{
    public class WrappedHttpClient : IApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        public string BaseApiServiceUrl { get; }

        public WrappedHttpClient(string url)
        {
            BaseApiServiceUrl = url;
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _client.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        {
            return await _client.PostAsync(requestUri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _client.DeleteAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content)
        {
            return await _client.PatchAsync(requestUri, content);
        }
    }
}