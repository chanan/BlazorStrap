using BlazorStrap.Util;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapCss(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<CurrentTheme>();
            serviceCollection.AddTransient<BlazorStrapInterop>();
            serviceCollection.AddTransient<IBootstrapCss, BootstrapCss>();
            serviceCollection.AddTransient<IBootstrapCSS, BootstrapCss>();
            return serviceCollection;
        }

        [Obsolete("AddBootstrapCSS is obsolete and will be removed in a future version of BlazorStrap. Please use AddBootstrapCss instead.", false)]
        public static IServiceCollection AddBootstrapCSS(this IServiceCollection serviceCollection) => AddBootstrapCss(serviceCollection);
    }
}
