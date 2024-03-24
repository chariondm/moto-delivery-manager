namespace MotoDeliveryManager.Adapters.Outbounds.AwsSQSMessageBroker;

public static class SqsMessageProducerSetup
{
    public static IServiceCollection AddSqsMessageProducer(this IServiceCollection services, IConfiguration configuration)
    {
        var awsOptions = configuration.GetAWSOptions();
        var queueUrl = configuration.GetSection("AWS:SQS:QueueName").Value!;
        var sqsConfig = new AmazonSQSConfig
        {
            ServiceURL = configuration.GetSection("AWS:SQS:ServiceURL").Value!,
            UseHttp = true
        };

        services
            .AddDefaultAWSOptions(awsOptions)
            .AddScoped<IQueueRentalAgreementRequestMessageBroker>(provider =>
            {
                var client = new AmazonSQSClient(sqsConfig);
                return new SqsMessageProducer(client, queueUrl, provider.GetRequiredService<ILogger<SqsMessageProducer>>()!);
            });

        return services;
    }
}
