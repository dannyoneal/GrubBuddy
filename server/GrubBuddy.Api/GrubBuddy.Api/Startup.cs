using GrubBuddy.Api.Attributes;
using GrubBuddy.DataAccess;
using GrubBuddy.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentValidation.AspNetCore;
using GrubBuddy.Api.Middleware;
using GrubBuddy.DataAccess.Interfaces;
using GrubBuddy.DataAccess.Auth0;

namespace GrubBuddy.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddTransient<IGrubsRepository>(provider =>
                new GrubsRepository(Configuration.GetSection("MongoConnection:ConnectionString").Value,
                Configuration.GetSection("MongoConnection:Database").Value));

            services.AddTransient<IAuth0Api>(provider => new Auth0Api(Configuration.GetSection("Auth0:ClientId").Value,
                Configuration.GetSection("Auth0:ClientSecret").Value,
                Configuration.GetSection("Auth0:Audience").Value,
                Configuration.GetSection("Auth0:TokenUrl").Value));

            services.AddSingleton<Auth0Repository, Auth0Repository>();

            var serviceProvider = services.BuildServiceProvider();

            services.AddSingleton<IUserApi>(provider =>
                new UserApi(serviceProvider.GetService<IAuthRepository>(), 
                Configuration.GetSection("Auth0:ClientUrl").Value));

            services.AddSingleton<IUserRepository, UserRepository>();

            serviceProvider = services.BuildServiceProvider();
            var userRepo = serviceProvider.GetService<IUserRepository>();
            services.AddMvc(options =>
                {
                    options.Filters.Add(new ValidationAttribute());
                    options.Filters.Add(new AuthorizationAttribute(userRepo));
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GrubValidator>());

            userRepo.LoadUsers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/GrubBuddyApi-{Date}.txt");

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=}/{action}/{name?}");
            });
        }
    }
}
