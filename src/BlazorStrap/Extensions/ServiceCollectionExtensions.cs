using BlazorStrap.Service;
using BlazorStrap.Utilities;
using Microsoft.Extensions.DependencyInjection;
using CurrentTheme = BlazorStrap.Utilities.CurrentTheme;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<BlazorStrapInterop>();
            serviceCollection.AddScoped<IBlazorStrap, BlazorStrapCore>();
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
