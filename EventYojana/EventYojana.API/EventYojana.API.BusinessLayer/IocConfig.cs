using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.BusinessLayer.Interfaces.Vendor;
using EventYojana.API.BusinessLayer.Managers.Commons;
using EventYojana.API.BusinessLayer.Managers.Vendor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventYojana.API.BusinessLayer
{
    public class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVendorAuthenticationManager, VendorAuthenticationManager>();
        }

    }
}
