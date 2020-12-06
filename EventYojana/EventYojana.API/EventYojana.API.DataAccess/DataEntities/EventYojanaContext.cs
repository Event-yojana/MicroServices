using EventYojana.API.DataAccess.DataEntities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.API.DataAccess.DataEntities
{
    public class EventYojanaContext : DbContext
    {
        public EventYojanaContext()
        {

        }

        public EventYojanaContext(DbContextOptions<EventYojanaContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot baseConfig = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();

            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(baseConfig.GetConnectionString("EventYojanaDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>().HasKey(x => x.LoginId);
            modelBuilder.Entity<EmailLogs>().HasKey(x => x.EmailLogId);
        }
    }
}
