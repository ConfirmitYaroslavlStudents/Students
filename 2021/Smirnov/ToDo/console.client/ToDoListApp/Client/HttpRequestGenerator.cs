using System.Net.Http;
using System.Threading.Tasks;

namespace ToDoListApp.Client
{
    public class HttpRequestGenerator
    {
        private readonly PathForRequest _pathForRequest;
        private readonly BodyForRequest _bodyForRequest;

        public HttpRequestGenerator()
        {
            _pathForRequest = new PathForRequest();
            _bodyForRequest = new BodyForRequest();
        }
        public async Task<HttpResponseMessage> GetAllTask()
        {
            using var client = new HttpClient();
            return await client.GetAsync(_pathForRequest.GetPathForGetRequest());
        }

        public async Task<HttpResponseMessage> AddTask(string description)
        {
            using var client = new HttpClient();
            return await client.PostAsync(_pathForRequest.GetPathForPostRequest(), _bodyForRequest.GetBodyForPostRequest(description));

        }

        public async Task<HttpResponseMessage> ChangeTaskDescription(long id, string description)
        {
            using var client = new HttpClient();
            return await client.PatchAsync(_pathForRequest.GetPathForPatchRequest(id), _bodyForRequest.GetBodyForPatchRequest(description));
        }

        public async Task<HttpResponseMessage> CompleteTask(long id)
        {
            using var client = new HttpClient();
            return await client.PatchAsync(_pathForRequest.GetPathForPatchRequest(id), _bodyForRequest.GetBodyForPatchRequest(1L));
        }

        public async Task<HttpResponseMessage> DeleteTask(long id)
        {
            using var client = new HttpClient();
            return await client.DeleteAsync(_pathForRequest.GetPathForDeleteRequest(id));
        }
    }
}
