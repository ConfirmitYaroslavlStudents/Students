using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoLibrary.Const;
using ToDoLibrary.Loggers;
using ToDoLibrary.SaveAndLoad;

namespace TodoWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IMediator>(GetMediator());
        }

        private WebMediator GetMediator()
        {
            var logger = new WebLogger();
            var saver = new FileSaver(Data.SaveAndLoadFileName);
            var loader = new FileLoader(Data.SaveAndLoadFileName, new FileLogger(Data.LogFileName));
            var requestHandler = new RequestHandler(logger, saver, loader);
            return new WebMediator(requestHandler);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
