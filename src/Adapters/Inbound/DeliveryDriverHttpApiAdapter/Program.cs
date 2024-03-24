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
    .AddPostgresDbAdapterDapperTypeHandlers()
    .AddPostgresDbAdapterDeliveryDriverRepository(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddPostgresDbAdapterRentalPlanRepository(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services
    .AddAwsS3StorageAdapter(builder.Configuration)
    .AddSqsMessageProducer(builder.Configuration);

builder.Services
    .AddRegisterDeliveryDriverUseCase()
    .AddListRentalPlansUseCase()
    .AddQueueRentalAgreementRequestUseCase();

var app = builder.Build();

app.UseCustomSwagger(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
