using Adapters.Inbound.SQSDriverLicensePhotoProcessorAdapter;
using Adapters.Outbounds.PostgresDbAdapter;

using Amazon.SQS;

using Core.Application.UseCases.ProcessDriverLicensePhotoUpload;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var config = new AmazonSQSConfig
{
    ServiceURL = builder.Configuration.GetSection("AWS:SQS:ServiceURL").Value!,
    UseHttp = true
};

var queueName = builder.Configuration.GetSection("AWS:SQS:QueueName").Value!;

builder.Services
    .AddDefaultAWSOptions(builder.Configuration.GetAWSOptions())
    .AddSingleton<IAmazonSQS>(sp => new AmazonSQSClient(config))
    .AddSingletonAddPostgresDbAdapterDeliveryDriverRepository(builder.Configuration.GetConnectionString("DefaultConnection")!)
    .AddSingletonProcessDriverLicensePhotoUploadUseCase();

builder.Services.AddHostedService(sp => new DriverLicensePhotoProcessorWorker(sp, queueName));

var host = builder.Build();
host.Run();
