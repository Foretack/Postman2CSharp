using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Postman2CSharp.Wasm;
using MudBlazor.Services;
using Postman2CSharp.Wasm.Services;
using MudBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress);

var host = builder.Build();
var wasmEnvironment = host.Services.GetService<IWebAssemblyHostEnvironment>();
if (wasmEnvironment?.Environment != "Prerendering")
{
    var lazyLoader = host.Services.GetService<LazyLoader>();
    if (lazyLoader != null)
    {
        await lazyLoader.OnNavigating();
    }

}
await host.RunAsync();

static void ConfigureServices(IServiceCollection services, string baseAddress)
{
    services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
    services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.NewestOnTop = false;
        config.SnackbarConfiguration.ShowCloseIcon = true;
        config.SnackbarConfiguration.VisibleStateDuration = 2000;
        config.SnackbarConfiguration.HideTransitionDuration = 500;
        config.SnackbarConfiguration.ShowTransitionDuration = 500;
        config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
    });
    services.AddScoped<Navigate>();
    services.AddScoped<Interop>();
    services.AddScoped<TabsService>();
    services.AddScoped(typeof(Lazy<>), typeof(Lazy<>));
    services.AddScoped<LazyLoader>();
}