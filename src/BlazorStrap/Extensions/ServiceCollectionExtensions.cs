
using BlazorStrap.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorStrap(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBlazorStrapService, BlazorStrapService>();
            serviceCollection.AddScoped<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
