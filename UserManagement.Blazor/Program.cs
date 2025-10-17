using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UserManagement.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<AppConfig>("appsettings.json");
builder.Services.AddScoped(x => new HttpClient
{
    BaseAddress = new Uri(config!.ApiUrl)
});

await builder.Build().RunAsync();

public class AppConfig
{
    public string ApiUrl { get; set; } = "";
}
