using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ToDoApp;

namespace ToDoListClientTests.Models
{
    public class HttpClientFake
    {
        public static HttpClient GetFakeHttpClient()
        {
            IEnumerable<ToDoItem> result = new[] { new ToDoItem { Description = "Water the plants", IsComplete = true, Id = 0 } };
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    var response = new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(JsonConvert.SerializeObject(result))
                    };
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                });

            return new HttpClient(mockHttpMessageHandler.Object) { BaseAddress = new Fixture().Create<Uri>() };
        }

        public static HttpClient GetFakeHttpClientThatReturnsBadRequest()
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            return new HttpClient(mockHttpMessageHandler.Object) { BaseAddress = new Fixture().Create<Uri>() };
        }
    }
}
