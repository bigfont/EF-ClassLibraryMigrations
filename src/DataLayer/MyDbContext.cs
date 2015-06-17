using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.ConfigurationModel;

namespace DataLayer
{
    public class SomeModel
    {
        public int Id { get; set; }
    }

    public class MyDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SomeModel>().Key(e => e.Id);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = Config["ConnectionStrings:MyDbContext"];
            optionsBuilder.UseSqlServer(connString);
        }

        public IConfiguration Config { get; set; }

        public MyDbContext()
        {
            var config = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Config = config;
        }
    }
}
