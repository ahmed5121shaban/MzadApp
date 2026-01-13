using FilterService.Application.Contracts.Mzad;
using FilterService.Extentions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MongoDB");
var dbName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Services
builder.Services.AddScoped<IMzadService, FilterService.Application.Services.Mzad.MzadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await MongoDbInit.InitMongoDb(dbName, connectionString);
await MongoDbInit.MongoIndexes();

app.Run();
