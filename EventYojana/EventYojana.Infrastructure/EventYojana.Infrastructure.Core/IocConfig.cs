using EventYojana.Infrastructure.Core.Common;
using EventYojana.Infrastructure.Core.Services;
using EventYojana.Infrastructure.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventYojana.Infrastructure.Core
{
    public static class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection services, IConfiguration configuration)
        {
            // configure strongly typed settings object
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<, > ();
        }

        //public static void SetConfigurationValues(IConfiguration configuration)
        //{
        //    var v = new CommonConfigurationSetting();
        //    configuration.GetSection("appSettings").Bind(v);
        //}
    }
}
