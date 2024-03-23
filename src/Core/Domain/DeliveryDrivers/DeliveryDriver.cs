namespace MotoDeliveryManager.Core.Domain.DeliveryDrivers;

/// <summary>
/// Represents a delivery driver.
/// </summary>
/// <param name="Id">The unique identifier of the delivery driver.</param>
/// <param name="Name">The name of the delivery driver.</param>
/// <param name="Cnpj">The CNPJ of the delivery driver.</param>
/// <param name="DateOfBirth">The date of birth of the delivery driver.</param>
/// <param name="DriverLicense">The driver's license of the delivery driver.</param>
/// <param name="CreatedAt">The date and time when the delivery driver was created.</param>
/// <param name="UpdatedAt">The date and time when the delivery driver was last updated.</param>
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
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    public DeliveryDriver(Guid id, string name, string cnpj, DateOnly dateOfBirth, DriverLicense driverLicense,
        DateTime createdAt, DateTime updatedAt)
        : this(id, name, cnpj, dateOfBirth, driverLicense)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryDriver"/> class.
    /// </summary>
    /// <remarks>
    /// This constructor is used by Entity Framework Core to create an instance of the <see cref="DeliveryDriver"/> class.
    /// </remarks>
    private DeliveryDriver() : this(Guid.Empty, string.Empty, string.Empty, new DateOnly(),
        new DriverLicense(string.Empty, DriverLicenseCategory.A, string.Empty), DateTime.UtcNow, DateTime.UtcNow)
    {
    }
};
