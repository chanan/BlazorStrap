using Microsoft.Extensions.DependencyInjection;

namespace BlazorStrap.Extensions
{
    /// <summary>
    /// Adds the SvgLoader to Razor's Dependency Injection System
    /// </summary>
    public static class SvgLoaderExtension
    {
        public static IServiceCollection AddSvgLoader(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISvgLoader, SvgLoader>();
            return serviceCollection;
        }
    }
}
