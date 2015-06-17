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
            var connString = this.Configuration["ConnectionStrings:MyDbContext"];
            optionsBuilder.UseSqlServer(connString);
        }

        public IConfiguration Configuration { get; set; }

        public MyDbContext()
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("config.json");
            this.Configuration = configuration;
        }
    }
}
