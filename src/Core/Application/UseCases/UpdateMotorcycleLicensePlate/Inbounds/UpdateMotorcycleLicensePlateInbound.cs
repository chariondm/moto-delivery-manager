namespace MotoDeliveryManager.Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;

/// <summary>
/// Represents the inbound data required for updating a motorcycle.
/// </summary>
/// <param name="MotorcycleId">Unique identifier for the motorcycle.</param>
/// <param name="LicensePlate">License plate of the motorcycle.</param>
public record UpdateMotorcycleLicensePlateInbound(Guid MotorcycleId, string LicensePlate);
