namespace MotoDeliveryManager.Core.Domain.DeliveryDrivers;

/// <summary>
/// Represents the driver's license of a delivery driver.
/// </summary>
/// <param name="Number">The driver's license number.</param>
/// <param name="Type">The driver's license type.</param>
/// <param name="PhotoPath">The path to the driver's license photo.</param>
/// <remarks>
/// This record is used to represent the driver's license of a delivery driver. It contains the driver's license number,
/// the driver's license type, and the path to the driver's license photo. The driver's license number and type are required
/// fields, but the photo path is optional.
/// </remarks>
/// <seealso cref="DriverLicenseCategory"/>
/// <seealso cref="DeliveryDriver"/>
public record DriverLicense(string Number, DriverLicenseCategory Category, string? PhotoPath);
