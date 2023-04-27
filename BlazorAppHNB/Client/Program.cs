using BlazorAppHNB.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorAppHNB.Client.Authentication;
using BlazorAppHNB.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp =>new HttpClient 
    { 
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    });

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<DataService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();


