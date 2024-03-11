namespace Core.Domain;

public record Motorcycle(Guid MotorcycleId, int Year, string Model, string LicensePlate)
{
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; init; } = DateTime.UtcNow;

    public Motorcycle(Guid motorcycleId, int year, string model, string licensePlate, DateTime createdAt, DateTime updatedAt)
        : this(motorcycleId, year, model, licensePlate)
    {
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
