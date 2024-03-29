﻿using EventYojana.API.DataAccess.DataEntities;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Admin;
using EventYojana.API.DataAccess.Interfaces.Common;
using EventYojana.API.DataAccess.Interfaces.Vendor;
using EventYojana.API.DataAccess.Repositories.Admin;
using EventYojana.API.DataAccess.Repositories.Common;
using EventYojana.API.DataAccess.Repositories.Vendor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventYojana.API.DataAccess
{
    public static class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection serviceCollection)
        {
            var sp = serviceCollection.BuildServiceProvider();
            var setingService = sp.GetService<IConfiguration>();
            string DbConnectionString = setingService.GetValue<string>("EventYojanaDb");
            serviceCollection.AddDbContext<EventYojanaContext>(options => options.UseSqlServer(DbConnectionString), ServiceLifetime.Transient);
            //serviceCollection.AddDbContext<EventYojanaContext>(options => options.UseSqlServer("data source=BHAVNA-PC\\BHAVNASQL;initial catalog=Event-Yojana-Management;persist security info=True;user id=sa;password=Password12@;"), ServiceLifetime.Transient);

            serviceCollection.AddScoped<IDatabaseContext, DatabaseContext>();
            serviceCollection.AddTransient<IAuthenticateRepository, AuthenticateRepository>();
            serviceCollection.AddTransient<IVendorAuthenticationRepository, VendorAuthenticationRepository>();
            serviceCollection.AddTransient<ILoggingRepository, LoggingRepository>();
            serviceCollection.AddTransient<IVendorRepository, VendorRepository>();

        }
    }
}
