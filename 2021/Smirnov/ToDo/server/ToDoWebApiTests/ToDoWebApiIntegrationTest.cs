using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoWebApi;
using ToDoWebApi.Database;
using ToDoWebApi.Models;

namespace ToDoWebApiTests
{
    public class ToDoWebApiIntegrationTest
    {
        private const string Path = "api/ToDoItems";
        private readonly WebApplicationFactory<Startup> _webHost;
        private readonly HttpClient _httpClient;
        private List<ToDoItem> _toDoItems = new List<ToDoItem>{new ToDoItem { Description = "test", Status = ToDoItemStatus.NotDone}, 
                                                               new ToDoItem { Description = "test1", Status = ToDoItemStatus.Done },
                                                               new ToDoItem { Description = "test2", Status = ToDoItemStatus.NotDone}
                                                               };
        public ToDoWebApiIntegrationTest()
        {
            _webHost = InitializeWebHost();
            _httpClient = _webHost.CreateClient();
        }
        [Fact]
        public async Task GetToDoItems_SendRequest_ShouldReturnOk()
        {
            //act
            var response = await _httpClient.GetAsync(Path);
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItem_SendRequest_ShouldReturnOkAndToDoItem()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.GetAsync(Path + "/1");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(StringFormatter("1", "test", "NotDone"), response.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task GetToDoItem_SendWrongId_ShouldReturnNotFound()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.GetAsync(Path + "/10");
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PostToDoItem_SendRequest_ShouldCreateToDoItem()
        {
            //Arrange
            var toDoContext = InitializeToDoContext();
            //act
            var response = await _httpClient.PostAsync(Path,
                           new StringContent($@"{{""description"" : ""testPost""}}", Encoding.UTF8, "application/json"));
            //assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(StringFormatter("4","testPost", "NotDone"), response.Content.ReadAsStringAsync().Result);
            var toDoItem = toDoContext.ToDoItems.Find(4L);
            Assert.Equal(4, toDoItem.Id);
            Assert.Equal("testPost", toDoItem.Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoItem.Status);
        }
        [Fact]
        public async Task PatchToDoItem_SendWrongId_ShouldReturnNotFound()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.PatchAsync(Path+ "/ChangeDescription/5",
                new StringContent($@"{{""description"" : ""testPatch""}}", Encoding.UTF8, "application/json"));
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PatchToDoItem_SendRequest_ShouldPatchDescriptionAndReturnNoContent()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.PatchAsync(Path + "/1",
                new StringContent($@"{{""description"" : ""testPatch""}}", Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            response = await _httpClient.GetAsync(Path + "/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(StringFormatter("1", "testPatch", "NotDone"), response.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task PatchToDoItem_SendRequest_ShouldPatchStatusAndReturnOk()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.PatchAsync(Path + "/1",
                new StringContent($@"{{""Status"" : ""Done""}}", Encoding.UTF8, "application/json"));
            var responseGet = await _httpClient.GetAsync(Path + "/1");
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.Equal(StringFormatter("1", "test", "Done"), responseGet.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task DeleteToDoItem_SendRequest_ShouldReturnNoContent()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.DeleteAsync(Path + "/1");
            var responseGet = await _httpClient.GetAsync(Path + "/1");
            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }
        [Fact]
        public async Task DeleteToDoItem_SendWrong_ShouldReturnNotFound()
        {
            //Arrange
            InitializeToDoContext();
            //act
            var response = await _httpClient.DeleteAsync(Path + "/5");
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        private WebApplicationFactory<Startup> InitializeWebHost()
        {
            return new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.SingleOrDefault(db =>
                        db.ServiceType == typeof(DbContextOptions<ToDoContext>));

                    services.Remove(dbContextDescriptor);

                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    services.AddDbContext<ToDoContext>(options =>
                    {
                        options.UseInMemoryDatabase("todo_db");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
                });
            });
        }

        private ToDoContext InitializeToDoContext()
        {
            var toDoContext = _webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            toDoContext.ToDoItems.AddRangeAsync(_toDoItems);
            toDoContext.SaveChangesAsync();
            return toDoContext;
        }

        private string StringFormatter(string id, string description, string status)
        {
            return $@"{{""Id"":{id},""Description"":""{description}"",""Status"":""{status}""}}";
        }
    }
}
