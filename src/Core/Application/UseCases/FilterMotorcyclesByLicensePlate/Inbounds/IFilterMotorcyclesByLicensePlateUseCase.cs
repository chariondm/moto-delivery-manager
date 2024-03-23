namespace MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;

/// <summary>
/// Processes the filtering of motorcycles by an optional license plate.
/// </summary>
public interface IFilterMotorcyclesByLicensePlateUseCase
{
    /// <summary>
    /// Executes the filtering operation.
    /// </summary>
    /// <param name="licensePlate">The license plate to filter by. If null or empty, all motorcycles are considered.</param>
    Task ExecuteAsync(string? licensePlate);

    /// <summary>
    /// Sets the outcome handler for the processing operation.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    void SetOutcomeHandler(IFilterMotorcyclesByLicensePlateOutcomeHandler outcomeHandler);
}
