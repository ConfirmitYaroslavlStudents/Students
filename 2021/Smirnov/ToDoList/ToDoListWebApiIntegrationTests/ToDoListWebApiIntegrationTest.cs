using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ToDoListLib.Models;
using ToDoListLib.SaveAndLoad;
using ToDoListWebApi;
using Xunit;

namespace ToDoListWebApiIntegrationTests
{
    public class ToDoListWebApiIntegrationTest
    {
        private const string Path = "api/ToDoList";
        [Fact]
        public async void CheckStatus_SendRequest_Get_ShouldReturnOk()
        {
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(_ => { });
            var httpClient = webHost.CreateClient();

            var response = await httpClient.GetAsync(Path);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void CheckStatus_SendRequest_Post_ShouldReturnOk()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var orderSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(ISaveAndLoad));

                    services.Remove(orderSaveAndLoad);

                    var mockService = new Mock<ISaveAndLoad>();

                    mockService.Setup(_ => _.Load()).Returns(() => new List<Task>());

                    services.AddTransient(_ => mockService.Object);
                });
            });
            var httpClient = webHost.CreateClient();
            var stringContent = new StringContent(@"{""description"" : ""test""}", Encoding.UTF8, "application/json");
            //act
            var response = await httpClient.PostAsync(Path, stringContent);
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void CheckStatus_SendRequest_PutWithId_ShouldReturnOk()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var orderSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(ISaveAndLoad));

                    services.Remove(orderSaveAndLoad);

                    var mockService = new Mock<ISaveAndLoad>();

                    mockService.Setup(_ => _.Load()).Returns(() => new List<Task> { new Task
                                                                                                        {
                                                                                                            Id = 0, 
                                                                                                            Description = "test", 
                                                                                                            Status = TaskStatus.NotDone
                                                                                                        }
                                                                                                    });

                    services.AddTransient(_ => mockService.Object);
                });
            });
            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PutAsync("api/ToDoList/0", new StringContent(@"""check""", Encoding.UTF8, "application/json"));
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void CheckStatus_SendRequest_DeleteWithId_ShouldReturnOk()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var orderSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(ISaveAndLoad));

                    services.Remove(orderSaveAndLoad);

                    var mockService = new Mock<ISaveAndLoad>();

                    mockService.Setup(_ => _.Load()).Returns(() => new List<Task> { new Task
                        {
                            Id = 0,
                            Description = "test",
                            Status = TaskStatus.NotDone
                        }
                    });

                    services.AddTransient(_ => mockService.Object);
                });
            });
            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.DeleteAsync("api/ToDoList/0");
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async void CheckStatus_SendRequest_PutWithIdAndStatus_ShouldReturnOk()
        {
            // Arrange
            var webHost = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var orderSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(ISaveAndLoad));

                    services.Remove(orderSaveAndLoad);

                    var mockService = new Mock<ISaveAndLoad>();

                    mockService.Setup(_ => _.Load()).Returns(() => new List<Task> { new Task
                        {
                            Id = 0,
                            Description = "test",
                            Status = TaskStatus.NotDone
                        }
                    });

                    services.AddTransient(_ => mockService.Object);
                });
            });
            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PutAsync("api/ToDoList/0/1", null);
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}