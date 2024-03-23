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

builder.Services
    .AddPostgresDbAdapterMotorcycleRepository(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services
    .AddFilterMotorcyclesByLicensePlateUseCase()
    .AddMotorcycleRegistrationUseCase()
    .AddUpdateMotorcycleLicensePlateUseCase();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
