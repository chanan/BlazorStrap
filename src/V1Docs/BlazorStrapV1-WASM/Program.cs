using BlazorPrettyCode;
using BlazorStrap;
using BlazorStrap.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using BlazorStrapV1_Docs;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddBootstrapCss();
            builder.Services.AddBlazorPrettyCode(defaults =>
            {
                defaults.DefaultTheme = "SolarizedDark";
                defaults.ShowLineNumbers = true;
            });
            builder.Services.AddSvgLoader();
            
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
