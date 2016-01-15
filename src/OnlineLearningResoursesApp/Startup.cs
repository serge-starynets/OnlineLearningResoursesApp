using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using OnlineLearningResourcesApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using OnlineLearningResourcesApp.ViewModels;
using OnlineLearningResourcesApp.Controllers.Api;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authentication.Cookies;
using System.Net;
using AutoMapper;

namespace OnlineLearningResourcesApp
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc()
                .AddJsonOptions(opt => {
                    // Make property names in Json serialization are handled with camelCase
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            services.AddIdentity<User, IdentityRole>(config => 
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 6;
                config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    // Implement api unauthorized behavior
                     OnRedirectToLogin = ctx =>
                     {
                         if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK )
                         {
                             ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                         }
                         else
                         {
                             ctx.Response.Redirect(ctx.RedirectUri);
                         }
                         return Task.FromResult(0);
                     }
                };
            })
            .AddEntityFrameworkStores<LearningContext>();

            services.AddLogging();

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<LearningContext>();


            services.AddTransient<LearningContextSeedData>();
            services.AddScoped<ILearningRepository, LearningRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, LearningContextSeedData seeder, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseIdentity();

            Mapper.Initialize(config =>
            {
                config.CreateMap<LearningPlan, LearningPlanViewModel>().ReverseMap();
                config.CreateMap<Course, CourseViewModel>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(

                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            await seeder.EnsureSeedDataAsync();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
