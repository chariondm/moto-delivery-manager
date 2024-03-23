namespace MotoDeliveryManager.Adapters.Outbounds.AwsS3StorageAdapter;

public class S3StorageService(IAmazonS3 s3Client, string bucketName) : IRegisterDeliveryDriverStorageService
{
    private readonly IAmazonS3 _s3Client = s3Client;
    private readonly string _bucketName = bucketName;

    public async Task<string> GeneratePresignedUrlToUploadDeliveryDriverLicensePhotoAsync(
        Guid deliveryDriverId,
        CancellationToken cancellationToken = default)
    {
        var request = new GetPreSignedUrlRequest()
        {
            BucketName = _bucketName,
            Key =  $"tmp/{deliveryDriverId}.png",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddMinutes(5),
            ContentType = "image/png"
        };

        request.Metadata.Add("x-amz-meta-delivery-driver-id", deliveryDriverId.ToString());

        return await _s3Client.GetPreSignedURLAsync(request);
    }
}
