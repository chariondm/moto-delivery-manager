namespace MotoDeliveryManager.Adapters.Outbounds.AwsS3StorageAdapter;

public static class AwsS3StorageAdapterSetup
{
    public static IServiceCollection AddAwsS3StorageAdapter(this IServiceCollection services, IConfiguration configuration)
    {
        var awsOptions = configuration.GetAWSOptions();
        var bucketName = configuration.GetSection("AWS:S3:BucketName").Value!;
        var s3Config = new AmazonS3Config
        {
            ServiceURL = configuration.GetSection("AWS:S3:ServiceURL").Value!,
            ForcePathStyle = true
        };

        services
            .AddDefaultAWSOptions(awsOptions)
            .AddScoped<IRegisterDeliveryDriverStorageService>(provider =>
            {
                var s3Client = new AmazonS3Client(s3Config);
                return new S3StorageService(s3Client, bucketName);
            });

        return services;
    }
}
