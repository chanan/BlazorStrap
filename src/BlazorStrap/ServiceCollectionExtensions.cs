using BlazorStrap.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorStrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBootstrapCSS(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient <BlazorStrapInterop>();
            serviceCollection.AddTransient<IBootstrapCSS, BootstrapCSS>();

            return serviceCollection;
        }
    }
}
