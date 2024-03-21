namespace Core.Application.UseCases.RegisterMotorcycle.Inbounds;

/// <summary>
/// Represents the inbound data required for registering a motorcycle.
/// </summary>
/// <param name="MotorcycleId">Unique identifier for the motorcycle.</param>
/// <param name="Year">Year of manufacture of the motorcycle.</param>
/// <param name="Model">Model of the motorcycle.</param>
/// <param name="LicensePlate">License plate of the motorcycle.</param>
public record MotorcycleRegistrationInbound(Guid MotorcycleId, int Year, string Model, string LicensePlate);
