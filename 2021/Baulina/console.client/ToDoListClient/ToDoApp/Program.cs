﻿using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.CustomClient;
using ToDoApp.Settings;

namespace ToDoApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<App>().Run(args);
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            var config = LoadConfiguration();
            services.Configure<ClientSettingsConfiguration>(config.GetSection("ClientSettings"));
            services.AddTransient<App>();
            services.AddTransient<Client>();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Settings/appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
