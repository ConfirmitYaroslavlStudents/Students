using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToDoApi.CustomExceptionMiddleware;
using ToDoApi.SaveAndLoad;

namespace ToDoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("https://localhost:5001", "http://localhost:5000").AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddSingleton<ILogger, FileLogger>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddScoped<IListSaveAndLoad, FileSaveAndLoad>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ErrorHandlingMiddleware)); ;

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseCors("ApiCorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
