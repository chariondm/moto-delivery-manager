namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.UpdateMotorcycleLicensePlate.Outbounds;

/// <summary>
/// Defines the repository interface for updating a motorcycle's license plate and checking for license plate uniqueness.
/// </summary>
public interface IUpdateMotorcycleLicensePlateRepository
{
    /// <summary>
    /// Asynchronously checks if a motorcycle with the specified license plate already exists in the database.
    /// </summary>
    /// <param name="licensePlate">The license plate to check for existence.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if a motorcycle with the specified license plate exists; otherwise, false.</returns>
    Task<bool> ExistsByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the license plate of a motorcycle identified by its unique identifier.
    /// </summary>
    /// <param name="motorcycleId">The unique identifier of the motorcycle to update.</param>
    /// <param name="newLicensePlate">The new license plate number to assign to the motorcycle.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous update operation. The task result may contain the updated motorcycle entity.</returns>
    Task<Motorcycle?> UpdateAsync(Guid motorcycleId, string newLicensePlate, CancellationToken cancellationToken = default);
}
