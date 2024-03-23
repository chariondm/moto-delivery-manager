namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.RegisterDeliveryDriver.Inbounds;

/// <summary>
/// Represents the inbound data required for registering a delivery driver.
/// </summary>
/// <param name="DeliveryDriverId">The unique identifier of the delivery driver.</param>
/// <param name="Name">The name of the delivery driver.</param>
/// <param name="Cnpj">The CNPJ of the delivery driver.</param>
/// <param name="DateOfBirth">The date of birth of the delivery driver.</param>
/// <param name="DriverLicenseNumber">The driver's license number of the delivery driver.</param>
/// <param name="DriverLicenseCategory">The driver's license category of the delivery driver.</param>
/// <remarks>
/// This record represents the inbound data required for registering a delivery driver. It is used to pass the
/// necessary data to the use case for the registration of a delivery driver.
/// </remarks>
public record RegisterDeliveryDriverInbound(Guid DeliveryDriverId,
    string Name,
    string Cnpj,
    DateOnly DateOfBirth,
    string DriverLicenseNumber,
    DriverLicenseCategory DriverLicenseCategory);
