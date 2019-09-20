using BlazorStrap.Util;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapCSS(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<CurrentTheme>();
            serviceCollection.AddTransient<BlazorStrapInterop>();
            serviceCollection.AddTransient<IBootstrapCSS, BootstrapCSS>();
            return serviceCollection;
        }
    }
}
