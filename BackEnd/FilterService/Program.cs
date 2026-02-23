using FilterService.Application.Contracts.Mzad;
using FilterService.Consumer;
using FilterService.Extentions;
using FilterService.Infrastructure.HttpClients;
using MassTransit;
using Polly;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MongoDB");
var dbName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// MassTransit and RabbitMQ configuration
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<MzadCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("Filter",false));
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

// Services
builder.Services.AddScoped<IMzadService, FilterService.Application.Services.Mzad.MzadService>();

// Http Clients
builder.Services.AddHttpClient<MzadServiceClient>().AddPolicyHandler(AsyncPolicy());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Initialize MongoDB and create indexes on application startup
app.Lifetime.ApplicationStarted.Register(async () =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application started at {Time}", DateTime.UtcNow);
    await MongoDbInit.InitMongoDb(dbName, connectionString);
    await MongoDbInit.MongoIndexes(app.Services);
});


app.Run();

// Polly Policy to retry failed HTTP requests to Mzad API every 5 seconds until it succeeds
static IAsyncPolicy<HttpResponseMessage> AsyncPolicy()
    => Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
        .RetryForeverAsync( retryAttempt => TimeSpan.FromSeconds(5));