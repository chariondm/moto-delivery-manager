namespace MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Outbounds;

/// <summary>
/// Repository for updating the driver's license photo path.
/// </summary>
/// <remarks>
/// This repository is used by the ProcessDriverLicensePhotoUpload use case to update the driver's license photo path.
/// </remarks>
public interface IProcessDriverLicensePhotoUploadRepository
{
    /// <summary>
    /// Updates the driver's license photo path asynchronously.
    /// </summary>
    /// <param name="deliveryDriverId">The unique identifier of the delivery driver.</param>
    /// <param name="photoPath">The path to the driver's license photo.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries updated.</returns>
    Task<int> UpdateDriverLicensePhotoPathAsync(
        Guid deliveryDriverId,
        string photoPath,
        CancellationToken cancellationToken = default);
}
