using System.ComponentModel.DataAnnotations;

using MotoDeliveryManager.Core.Domain.DeliveryDrivers;

namespace Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.RegisterDeliveryDriver.V1;

/// <summary>
/// Represents the request to register a delivery driver.
/// </summary>
/// <param name="Name">The name of the delivery driver.</param>
/// <param name="Cnpj">The CNPJ of the delivery driver.</param>
/// <param name="DateOfBirth">The date of birth of the delivery driver.</param>
/// <param name="DriverLicenseNumber">The driver's license number of the delivery driver.</param>
/// <param name="DriverLicenseCategory">The driver's license type of the delivery driver.</param>
/// <remarks>
/// This record represents the request to register a delivery driver. It is used to pass the necessary data to the
/// delivery driver registration endpoint.
/// </remarks>
public record RegisterDeliveryDriverRequest
(
    [Required] string Name,
    [Required][Length(14, 14)] string Cnpj,
    [Required] DateOnly DateOfBirth,
    [Required] string DriverLicenseNumber,
    [Required] DriverLicenseCategory DriverLicenseCategory
);
