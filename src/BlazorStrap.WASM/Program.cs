using BlazorStrap_Docs;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorStrap;
using BlazorStrap_Docs.Service;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ) });
builder.Services.AddScoped<Core>();
builder.Services.AddBlazorStrap(options =>
{
    options.ShowDebugMessages = true;
});
await builder.Build().RunAsync();
