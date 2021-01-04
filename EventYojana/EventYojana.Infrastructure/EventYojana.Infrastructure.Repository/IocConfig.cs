using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventYojana.Infrastructure.Repository
{
    public static class IocConfig
    {
        public static void ConfigureServices(ref IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepositoryProvider, RepositoryProvider>();
        }
    }
}
