using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MyToDoList;
using ToDoApi;

namespace ToDoListTestProject.ApiIntegrationTests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public ToDoList ToDoList { get; private set; } = new ToDoList { "Test task", "Another test task" };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                var orderSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(IListSaveAndLoad));

                services.Remove(orderSaveAndLoad);

                var mockService = new Mock<IListSaveAndLoad>();

                mockService.Setup(_ => _.LoadTheList()).Returns(() => ToDoList);
                mockService.Setup(_ => _.SaveTheList(It.IsAny<IEnumerable<ToDoItem>>())).Callback(
                    (IEnumerable<ToDoItem> toDoList) =>
                    {
                        ToDoList = new ToDoList(toDoList);
                    });

                services.AddTransient(_ => mockService.Object);
            });
        }
    }
}
