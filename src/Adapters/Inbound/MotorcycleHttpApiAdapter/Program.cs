using Adapters.Inbound.MotorcycleHttpApiAdapter.Modules.Common.Swagger;
using Adapters.Outbounds.PostgresDbAdapter;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate;
using Core.Application.UseCases.RegisterMotorcycle;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate;

using System.Text.Json.Serialization;

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

builder.Services.AddPostgresDbAdapter(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services
    .AddFilterMotorcyclesByLicensePlateUseCase()
    .AddMotorcycleRegistrationUseCase()
    .AddUpdateMotorcycleLicensePlateUseCase();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
