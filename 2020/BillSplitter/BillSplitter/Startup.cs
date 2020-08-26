using System;
using BillSplitter.Data;
using BillSplitter.Oauth;
using BillSplitter.Attributes;
using BillSplitter.Validation.ValidationHandlers;
using BillSplitter.Validation.ValidationMiddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BillSplitter.Controllers;

namespace BillSplitter
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            services.AddDbContext<BillContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("BillContext")));

            services.AddScoped<UnitOfWork>();

            services.AddScoped<SignInManager>();

            services.AddScoped<ValidateUserAttribute>();

            services.AddSingleton<RoleHandler>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                })
                .AddGoogle(options =>
                {
                    options.ClientId = Configuration["App:GoogleClientId"];
                    options.ClientSecret = Configuration["App:GoogleClientSecret"];

                })
                .AddFacebook(options =>
                {
                    options.AppId = Configuration["App:FBClientId"];
                    options.AppSecret = Configuration["App:FBClientSecret"];
                })
                .AddOAuth<VKOAuthOptions, VKOAuthHandler>("VK", "Vkontakte", options =>
                 {
                     options.ClientId = Configuration["App:VKClientId"];
                     options.ClientSecret = Configuration["App:VKClientSecret"];
                 });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Bills/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseMiddleware<ValidationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
