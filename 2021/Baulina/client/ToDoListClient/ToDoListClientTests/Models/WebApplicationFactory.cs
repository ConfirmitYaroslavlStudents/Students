using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ToDoApp;

namespace ToDoListClientTests.Models
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public List<ToDoItem> ToDoList { get; private set; } = new List<ToDoItem>() { "Test task", "Another test task" };

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // will be called after the `ConfigureServices` from the Startup
            builder.ConfigureTestServices(services =>
            {
                var defaultSaveAndLoad = services.SingleOrDefault(d => d.ServiceType == typeof(IListSaveAndLoad));
                var defaultILogger = services.SingleOrDefault(d => d.ServiceType == typeof(ILogger));

                services.Remove(defaultSaveAndLoad);
                services.Remove(defaultILogger);

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
