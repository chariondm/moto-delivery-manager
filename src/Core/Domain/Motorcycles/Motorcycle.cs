namespace MotoDeliveryManager.Core.Domain.Motorcycles;

/// <summary>
/// Represents a motorcycle.
/// </summary>
/// <param name="MotorcycleId">The unique identifier of the motorcycle.</param>
/// <param name="Year">The year of the motorcycle.</param>
/// <param name="Model">The model of the motorcycle.</param>
/// <param name="LicensePlate">The license plate of the motorcycle.</param>
/// <returns>A motorcycle.</returns>
/// <remarks>Represents a motorcycle that can be rented.</remarks>
public record Motorcycle(Guid MotorcycleId, int Year, string Model, string LicensePlate);
