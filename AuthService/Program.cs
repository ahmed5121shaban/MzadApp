using AuthService.Core.Extensions;
using AuthService.Core.Helpers;
using AuthService.Infrastracture.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// DI Container Registration
builder.Services.DiContaonerConfig();

// DB configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseOpenIddict();
});

// Identity configuration
builder.IdentityConfig(connectionString);

// OpenIdDict configuration
builder.OpenIdDictConfig();

// SeedHelper

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    await SeedHelper.SeedAsync(app.Services);

app.Run();
