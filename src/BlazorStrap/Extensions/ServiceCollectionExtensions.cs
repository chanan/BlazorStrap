using BlazorStrap.Service;
using BlazorStrap.Utilities;
using Microsoft.Extensions.DependencyInjection;
using CurrentTheme = BlazorStrap.Utilities.CurrentTheme;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection, Action<BlazorStrapOptions>? options = null)
        {
            serviceCollection.AddScoped<BlazorStrapInterop>();
            serviceCollection.AddScoped<Interop>();
            serviceCollection.AddScoped<IBlazorStrap>(x => new BlazorStrapCore(x.GetRequiredService<BlazorStrapInterop>(), options, x.GetRequiredService<Interop>()));
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
