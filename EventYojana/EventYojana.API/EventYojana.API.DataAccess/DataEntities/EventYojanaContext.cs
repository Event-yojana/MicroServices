using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
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

        public virtual DbSet<EmailLogs> EmailLogs { get; set; }
        public virtual DbSet<UserLogin> UserLogin { get; set; }
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
            modelBuilder.Entity<Address>().HasKey(x => x.AddressId);
            modelBuilder.Entity<UserLogin>().HasKey(x => x.LoginId);
            modelBuilder.Entity<EmailLogs>().HasKey(x => x.EmailLogId);
            modelBuilder.Entity<Module>().HasKey(x => x.Id);
            modelBuilder.Entity<RoleModule>().HasKey(x => x.Id);
            modelBuilder.Entity<VendorDetails>().HasKey(x => x.VendorId);
        }
    }
}
