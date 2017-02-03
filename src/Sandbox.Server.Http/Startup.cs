using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Sandbox.Server.BusinessLogic.Handlers;
using Sandbox.Server.DataAccess.Repositories;
using Sandbox.Server.DomainObjects.Interfaces.Handlers;
using Sandbox.Server.DomainObjects.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Sandbox.Server.Http
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // Data access dependency injection
            services.AddSingleton<IPersonRepository, PersonRepository>();

            // Business logic dependency injection
            services.AddSingleton<IPersonHandler, PersonHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API v1", Version = "API v1" });
                // Add new API version below
                //c.SwaggerDoc("v2", new Info { Title = "My API v2", Version = "API v2" });

                c.DocInclusionPredicate((docName, api) => {
                    return api.RelativePath.Contains(docName);
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Open database connections
            DataAccess.Repositories.Abstract.MongoCollectionHandler
                .OpenConnections(
                    Configuration.GetConnectionString("readOnlyAccess"),
                    Configuration.GetConnectionString("writeAccess"));

            app.UseMvc();

            if(env.IsDevelopment()){
                app.UseSwagger();

                app.UseSwaggerUi(c => {
                    //c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
                    // Add new Api version above
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                });
            }
        }
    }
}
