using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.ConfigurationModel;
using DataLayer;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Hosting;

namespace WebApp
{
    public class Startup
    {
        public IConfiguration Config { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var config = new Configuration()
                .AddJsonFile("config.json")
                .AddUserSecrets()
                .AddEnvironmentVariables();

            Config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<MyDbContext>(options =>
                {
                    options.UseSqlServer(Config["ConnectionStrings:MyDbContext"]);
                });
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MyDbContext>();
            db.Database.AsSqlServer().EnsureCreated();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
