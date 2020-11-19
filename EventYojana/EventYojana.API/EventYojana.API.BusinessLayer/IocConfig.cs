using EventYojana.API.BusinessLayer.Interfaces.Commons;
using EventYojana.API.BusinessLayer.Managers.Commons;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventYojana.API.BusinessLayer
{
    public class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

    }
}
