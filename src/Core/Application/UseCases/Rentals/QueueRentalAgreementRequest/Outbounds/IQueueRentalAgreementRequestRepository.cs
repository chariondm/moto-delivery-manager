namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Outbounds;

/// <summary>
/// Defines the contract for the repository verifying the delivery driver exists.
/// </summary>
/// <remarks>
/// This repository is responsible for verifying if the delivery driver exists by the given id and driver license category.
/// </remarks>
public interface IQueueRentalAgreementRequestDeliveryDriverRepository
{
    /// <summary>
    /// Verifies if the delivery driver exists by the given id and driver license category.
    /// </summary>
    /// <param name="deliveryDriverId">The delivery driver id.</param>
    /// <param name="driverLicenseCategory">The driver license category.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    /// <remarks>It is used to verify if the delivery driver exists by the given id and driver license category.</remarks>
    /// <seealso cref="DeliveryDriver"/>
    /// <seealso cref="DriverLicenseCategory"/>
    /// <seealso cref="CancellationToken"/>
    Task<bool> VerifyDeliveryDriverExistsAsync(
        Guid deliveryDriverId,
        DriverLicenseCategory driverLicenseCategory,
        CancellationToken cancellationToken);
}
