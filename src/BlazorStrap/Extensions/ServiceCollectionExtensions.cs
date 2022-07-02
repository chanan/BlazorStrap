using BlazorStrap.Service;
using BlazorStrap.Utilities;
using Microsoft.Extensions.DependencyInjection;
using CurrentTheme = BlazorStrap.Utilities.CurrentTheme;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection, BootStrapVersion version = BootStrapVersion.Bootstrap5)
        {
            serviceCollection.AddScoped<BlazorStrapInterop>();
            serviceCollection.AddScoped<IBlazorStrap>(x => new BlazorStrapCore(x.GetRequiredService<BlazorStrapInterop>(), version));
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
