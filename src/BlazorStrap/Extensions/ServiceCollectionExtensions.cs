using BlazorStrap.Service;
using BlazorStrap.Utilities;
using Microsoft.Extensions.DependencyInjection;
using CurrentTheme = BlazorStrap.Utilities.CurrentTheme;

namespace BlazorStrap.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection, Action<BlazorStrapOptions>? options = null)
        {
            serviceCollection.AddScoped<BlazorStrapInterop>();
            serviceCollection.AddScoped<IBlazorStrap>(x => new BlazorStrapCore(x.GetRequiredService<BlazorStrapInterop>(), options));
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
