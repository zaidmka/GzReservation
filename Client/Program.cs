global using GzReservation.Client.Services.FormService;
global using GzReservation.Client.Services.SecurityFormService;
global using GzReservation.Shared;
global using GzReservation.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
global using GzReservation.Client.Services.OracleService;
global using GzReservation.Client.Services.ReservationService;
global using GzReservation.Client.DTOs;
global using GzReservation.Client.Services.MessageService;
global using GzReservation.Client.Services.AnnouncementService;

using GzReservation.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<ISecurityFormService, SecurityFormService>();
builder.Services.AddScoped<IOracleService, OracleService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddMudServices();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
await builder.Build().RunAsync();
