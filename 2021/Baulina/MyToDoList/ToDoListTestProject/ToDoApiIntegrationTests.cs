using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ToDoApi;
using Xunit;

namespace ToDoListTestProject
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }

    public class ToDoApiIntegrationTests:IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public ToDoApiIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWithIntParameterReturnsBadRequest()
        {
            var request = "todoItems/8";

            var response = await _client.GetAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task GetWithStringParameterReturnsBadRequest()
        {
            var request = "todoItems/something";

            var response = await _client.GetAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithIncorrectBodyReturnsBadRequest()
        {
            var request = new
            {
                Url = "todoItems",
                Body = 8
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostWithParametersAndBodyIsNotAllowed()
        {
            var request = new
            {
                Url = "todoItems/something",
                Body = 8
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task PostWithParametersIsNotAllowed()
        {
            var request = "todoItems/something";

            var response = await _client.PostAsync(request, null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithIncorrectParametersReturnsBadRequest()
        {
            var request = "todoItems/something";

            var response = await _client.DeleteAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithoutParametersIsNotAllowed()
        {
            var request = "todoItems";

            var response = await _client.DeleteAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithOutOfRangeParametersReturnsNotFound()
        {
            var request = "todoItems/-1";

            var response = await _client.DeleteAsync(request);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutWithoutParametersWithIncorrectBodyReturnsBadRequest()
        {
            var request = new
            {
                Url = "todoItems",
                Body = 5
            };

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutWithoutParametersAndBodyReturnsUnsupportedMediaType()
        {
            var request = "todoItems";

            var response = await _client.PutAsync(request, null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task PutWithIncorrectParameterReturnsBadRequest()
        {
            var request = "todoItems/something";

            var response = await _client.PutAsync(request, null);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetWithCorrectParameterReturnsOk()
        {
            var request = "todoItems";

            var response = await _client.GetAsync(request);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostWithCorrectParameterReturnsOk()
        {
            var request = new
            {
                Url = "todoItems",
                Body = "Walk the dog"
            };

            var response = await _client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteWithCorrectParameterReturnsOk()
        {
            var request = "todoItems/0";

            var response = await _client.DeleteAsync(request);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PutWithCorrectBodyReturnsOk()
        {
            var request = new
            {
                Url = "todoItems",
                Body = new
                {
                    Index = 0,
                    NewDescription = "Walk the dog"
                }
            };

            var response = await _client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
