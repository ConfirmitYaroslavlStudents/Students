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
        [Fact]
        public async Task GetToDoItems_SendRequest_ShouldReturnOk()
        {
            //Arrange
            var webHost = InitializeWebHost();
            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.GetAsync(Path);
            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetToDoItem_SendRequest_ShouldReturnOkAndToDoItem()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> {new ToDoItem(){Description = "test"}};
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.GetAsync(Path + "/1");

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(StringFormatter("1", "test", "0"), response.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task GetToDoItem_SendWrongId_ShouldReturnNotFound()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem() { Description = "test" } };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.GetAsync(Path + "/2");
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PostToDoItem_SendRequest_ShouldCreateToDoItem()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PostAsync(Path,
                           new StringContent($@"{{""description"" : ""testPost""}}", Encoding.UTF8, "application/json"));
            //assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(StringFormatter("1","testPost", "0"), response.Content.ReadAsStringAsync().Result);
            var toDoItem = dbContext.ToDoItems.Find(1L);
            Assert.Equal(1, toDoItem.Id);
            Assert.Equal("testPost", toDoItem.Description);
            Assert.Equal(ToDoItemStatus.NotDone, toDoItem.Status);
        }
        [Fact]
        public async Task PatchToDoItem_SendWrongId_ShouldReturnNotFound()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem{ Description = "test" } };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PatchAsync(Path+ "/ChangeDescription/5",
                new StringContent($@"{{""description"" : ""testPatch""}}", Encoding.UTF8, "application/json"));
            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task PatchToDoItem_SendRequest_ShouldPatchDescriptionAndReturnNoContent()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem { Description = "test" } };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PatchAsync(Path + "/1",
                new StringContent($@"{{""description"" : ""testPatch""}}", Encoding.UTF8, "application/json"));
            var responseGet = await httpClient.GetAsync(Path + "/1");

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.Equal(StringFormatter("1", "testPatch", "0"), responseGet.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task PatchToDoItem_SendRequest_ShouldPatchStatusAndReturnNoContent()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem { Description = "test", Status = ToDoItemStatus.NotDone} };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.PatchAsync(Path + "/1",
                new StringContent($@"{{""Status"" : 1}}", Encoding.UTF8, "application/json"));
            var responseGet = await httpClient.GetAsync(Path + "/1");
            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.Equal(StringFormatter("1", "test", "1"), responseGet.Content.ReadAsStringAsync().Result);
        }
        [Fact]
        public async Task DeleteToDoItem_SendRequest_ShouldReturnNoContent()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem { Description = "test", Status = ToDoItemStatus.NotDone } };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.DeleteAsync(Path + "/1");
            var responseGet = await httpClient.GetAsync(Path + "/1");
            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            Assert.Equal(HttpStatusCode.NotFound, responseGet.StatusCode);
        }
        [Fact]
        public async Task DeleteToDoItem_SendWrong_ShouldReturnNotFound()
        {
            //Arrange
            var webHost = InitializeWebHost();

            var dbContext = webHost.Services.CreateScope().ServiceProvider.GetService<ToDoContext>();
            var toDoItems = new List<ToDoItem> { new ToDoItem { Description = "test", Status = ToDoItemStatus.NotDone } };
            await dbContext.ToDoItems.AddRangeAsync(toDoItems);
            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();
            //act
            var response = await httpClient.DeleteAsync(Path + "/5");
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

        private string StringFormatter(string id, string description, string status)
        {
            return $@"{{""Id"":{id},""Description"":""{description}"",""Status"":{status}}}";
        }
    }
}
