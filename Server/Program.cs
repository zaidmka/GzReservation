global using Microsoft.EntityFrameworkCore;
global using GzReservation.Server.Services.FormService;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.ResponseCompression;
global using System.Configuration;
global using GzReservation.Server.Services.EntityService;

global using GzReservation.Server.Services.AuthService;
global using GzReservation.Server.Services.OracleService;

global using GzReservation.Shared;
global using GzReservation.Server.DTOs;

global using GzReservation.Server.Data;
global using GzReservation.Server.Services.TestService;
global using GzReservation.Server.Services.ReservationService;
global using GzReservation.Server.Services.AnnouncementService;

using Microsoft.AspNetCore.Builder;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AspNetCoreRateLimit;
using System;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

//use the .env
builder.Configuration.AddEnvironmentVariables();

var connectionString = builder.Configuration["POSTGRES_CONNECTION"];
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

Console.WriteLine(" From .env: " + connectionString);

//builder.Services.AddDbContext<DataContext>(options =>
//{ 
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); 

//});
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IOracleService, OracleService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IEntityService, EntityService>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminRolePolicy", policy =>
    {
        policy.RequireAssertion(context =>
        {
            // Check if the user has any role that contains "admin"
            return context.User.Claims.Any(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.Value.ToLower().Contains("admin"));
        });
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

//protect from brute-force attack
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
var app = builder.Build();
//Middleware Protection
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self'; style-src 'self' 'unsafe-inline'; script-src 'self' 'unsafe-inline' 'unsafe-eval'");

    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//active the brute-force protection
app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerUI();
app.UseSwagger();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
