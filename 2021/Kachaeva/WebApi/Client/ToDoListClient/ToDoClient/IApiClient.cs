using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoClient
{
    public interface IApiClient
    {
        public Task<HttpResponseMessage> GetAsync(string requestUri);
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
        public Task<HttpResponseMessage> DeleteAsync(string requestUri);
        public Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content);
    }
}
