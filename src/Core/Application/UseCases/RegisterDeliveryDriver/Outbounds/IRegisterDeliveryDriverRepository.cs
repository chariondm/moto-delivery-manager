using Core.Domain.DeliveryDrivers;

namespace Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;

/// <summary>
/// Defines the contract for the repository that registers a delivery driver.
/// </summary>
/// <remarks>
/// This repository is responsible for registering a delivery driver.
/// </remarks>
/// <seealso cref="DeliveryDriver"/>
public interface IRegisterDeliveryDriverRepository
{
    /// <summary>
    /// Checks if the CNPJ or driver's license number is already in use.
    /// </summary>
    /// <param name="cnpj">The CNPJ to check.</param>
    /// <param name="driverLicenseNumber">The driver's license number to check.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A boolean value indicating whether the CNPJ or driver's license number is already in use.</returns>
    /// <remarks>
    /// This method checks if the CNPJ or driver's license number is already in use by another delivery driver.
    /// </remarks>
    Task<bool> IsCnpjOrDriverLicenseNumberInUseAsync(string cnpj, string driverLicenseNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a delivery driver.
    /// </summary>
    /// <param name="deliveryDriver">The delivery driver to register.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// This method registers a delivery driver.
    /// </remarks>
    /// <seealso cref="DeliveryDriver"/>
    Task RegisterDeliveryDriverAsync(DeliveryDriver deliveryDriver, CancellationToken cancellationToken = default);
}
