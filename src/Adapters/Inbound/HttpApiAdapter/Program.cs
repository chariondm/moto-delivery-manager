using Adapters.Outbounds.PostgresDbAdapter;
using Core.Application.UseCases.RegisterMotorcycle;

using System.Text.Json.Serialization;

using Adapters.Inbound.HttpApiAdapter.Modules.Common.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder
    .Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddMotorcycleRepositoryRepository(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddMotorcycleRegistrationUseCase();


var app = builder.Build();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
