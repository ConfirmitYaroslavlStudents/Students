using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ToDoApi;
using ToDoApi.Models;
using ToDoApi.SaveAndLoad;

namespace ToDoListServerTests.IntegrationTests
{
    public class WebApplicationFactory : WebApplicationFactory<Startup>
    {
        public ToDoList ToDoList { get; private set; } = new ToDoList { "Test task", "Another test task" };
        private readonly LoggerFake _loggerFake = new LoggerFake();

        public WebApplicationFactory() { }
        public WebApplicationFactory(LoggerFake loggerFake)
        {
            _loggerFake = loggerFake;
        }

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
                services.AddSingleton<ILogger>(_loggerFake);
            });
        }
    }
}
