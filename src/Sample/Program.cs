using BlazorPrettyCode;
using BlazorStrap;
using BlazorStyled;
using Microsoft.AspNetCore.Blazor.Hosting;
using SampleCore;
using System.Threading.Tasks;

namespace Sample
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazorPrettyCode(defaults =>
            {
                defaults.DefaultTheme = "SolarizedDark";
                defaults.ShowLineNumbers = true;
            });
            builder.Services.AddBootstrapCss();

            builder.RootComponents.Add<App>("app");
            builder.RootComponents.Add<ClientSideStyled>("#styled");

            await builder.Build().RunAsync();
        }
    }
}
