using Core.Domain.Motorcycles;

namespace Core.Application.UseCases.RegisterMotorcycle.Outbounds;

/// <summary>
/// Provides an interface for accessing and manipulating motorcycle records.
/// </summary>
public interface IRegisterMotorcycleRepository
{
    /// <summary>
    /// Checks if a motorcycle with the specified license plate already exists.
    /// </summary>
    /// <param name="licensePlate">The license plate to check.</param>
    /// <returns>True if a motorcycle with the specified license plate exists; otherwise, false.</returns>
    Task<bool> ExistsByLicensePlateAsync(string licensePlate);

    /// <summary>
    /// Registers a new motorcycle.
    /// </summary>
    /// <param name="motorcycle">The motorcycle to register.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RegisterAsync(Motorcycle motorcycle);
}

