using EventYojana.Infrastructure.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventYojana.Infrastructure.Core
{
    public static class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection services)
        {
            //services.AddScoped<, > ();
        }

        public static void SetConfigurationValues(IConfiguration configuration)
        {
            var v = new CommonConfigurationSetting();
            configuration.GetSection("appSettings").Bind(v);
        }
    }
}
