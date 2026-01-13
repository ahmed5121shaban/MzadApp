using Microsoft.EntityFrameworkCore;
using MzadService.Application.Contracts;
using MzadService.Application.Contracts.Mzad;
using MzadService.Application.Services;
using MzadService.Data.DataSeeding;
using MzadService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddScoped<IMzadService, MzadService.Application.Services.Mzad.MzadService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefualtConnection"));
});

var app = builder.Build();

// Initialize and seed the database
DbInitializer.InitializeDbAsync(app).Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
