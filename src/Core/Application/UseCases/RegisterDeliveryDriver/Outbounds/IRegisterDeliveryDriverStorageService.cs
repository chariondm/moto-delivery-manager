namespace Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;

// I need a interface to represent the storage service that will be used to generate a presigned url to upload the delivery driver license photo such as AWS S3, Azure Blob Storage, Google Cloud Storage, etc.
/// <summary>
/// Represents the storage service that will be used to generate a presigned url to upload the delivery driver license photo.
/// </summary>
/// <remarks>
/// This interface is used to abstract the storage service that will be used to generate a presigned url to upload the delivery driver license photo.
/// </remarks>
public interface IRegisterDeliveryDriverStorageService
{
    /// <summary>
    /// Generates a presigned url to upload the delivery driver license photo.
    /// </summary>
    /// <param name="deliveryDriverId">The delivery driver id.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<string> GeneratePresignedUrlToUploadDeliveryDriverLicensePhotoAsync(Guid deliveryDriverId, CancellationToken cancellationToken = default);
}
