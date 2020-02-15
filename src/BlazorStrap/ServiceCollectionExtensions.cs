using BlazorStrap.Util;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapCss(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<CurrentTheme>();
            serviceCollection.AddTransient<BlazorStrapInterop>();
            serviceCollection.AddTransient<IBootstrapCss, BootstrapCss>();
            return serviceCollection;
        }
    }
}
