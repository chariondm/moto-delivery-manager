using Adapters.Inbound.SQSDriverLicensePhotoProcessorAdapter;

using Amazon.SQS;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var awsOptions = builder.Configuration.GetAWSOptions();
var config = new AmazonSQSConfig
    {
        ServiceURL = builder.Configuration.GetSection("AWS:SQS:ServiceURL").Value!,
        UseHttp = true
    };
var queueName = builder.Configuration.GetSection("AWS:SQS:QueueName").Value!;

builder.Services
    .AddDefaultAWSOptions(awsOptions)
    .AddSingleton<IAmazonSQS>(sp => new AmazonSQSClient(config));

builder.Services.AddHostedService(sp => 
    new DriverLicensePhotoProcessorWorker(
        sp.GetRequiredService<IAmazonSQS>(),
        queueName,
        sp.GetRequiredService<ILogger<DriverLicensePhotoProcessorWorker>>()));


var host = builder.Build();
host.Run();
