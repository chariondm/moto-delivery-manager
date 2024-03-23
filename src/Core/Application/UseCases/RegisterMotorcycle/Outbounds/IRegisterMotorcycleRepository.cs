using MotoDeliveryManager.Core.Domain.Motorcycles;

namespace MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Outbounds;

/// <summary>
/// Provides an interface for accessing and manipulating motorcycle records.
/// </summary>
public interface IRegisterMotorcycleRepository
{
    /// <summary>
    /// Checks if a motorcycle with the specified license plate already exists.
    /// </summary>
    /// <param name="licensePlate">The license plate to check.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>True if a motorcycle with the specified license plate exists; otherwise, false.</returns>
    Task<bool> ExistsByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a new motorcycle.
    /// </summary>
    /// <param name="motorcycle">The motorcycle to register.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RegisterAsync(Motorcycle motorcycle, CancellationToken cancellationToken = default);
}

