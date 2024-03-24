namespace MotoDeliveryManager.Core.Domain.DeliveryDrivers;

/// <summary>
/// Represents a delivery driver.
/// </summary>
/// <param name="Id">The unique identifier of the delivery driver.</param>
/// <param name="Name">The name of the delivery driver.</param>
/// <param name="Cnpj">The CNPJ of the delivery driver.</param>
/// <param name="DateOfBirth">The date of birth of the delivery driver.</param>
/// <param name="DriverLicense">The driver's license of the delivery driver.</param>
/// <remarks>
/// This record is used to represent a delivery driver.
/// </remarks>
/// <seealso cref="DriverLicense"/>
public record DeliveryDriver(
    Guid Id,
    string Name,
    string Cnpj,
    DateOnly DateOfBirth,
    DriverLicense DriverLicense
    )
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryDriver"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is used by Entity Framework Core to create an instance of the <see cref="DeliveryDriver"/> class.
    /// </remarks>
    private DeliveryDriver() : this(Guid.Empty, string.Empty, string.Empty, new DateOnly(),
        new DriverLicense(string.Empty, DriverLicenseCategory.A, string.Empty))
    {
    }
};
