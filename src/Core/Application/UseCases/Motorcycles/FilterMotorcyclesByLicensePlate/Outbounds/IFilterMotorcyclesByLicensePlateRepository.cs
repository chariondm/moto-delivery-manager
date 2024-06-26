namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.FilterMotorcyclesByLicensePlate.Outbounds;

/// <summary>
/// Provides an interface for accessing and manipulating motorcycle records, allowing for optional filtering by license plate.
/// </summary>
public interface IFilterMotorcyclesByLicensePlateRepository
{
    /// <summary>
    /// Retrieves motorcycles optionally filtered by license plate. If the license plate is null or empty, all motorcycles are retrieved.
    /// </summary>
    /// <param name="licensePlate">The license plate to filter motorcycles by. This parameter is optional.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of motorcycles that match the filter criteria. An empty list is returned if no motorcycles match the criteria or if the criteria are not specified.</returns>
    Task<IEnumerable<Motorcycle>> FindByLicensePlateAsync(string? licensePlate, CancellationToken cancellationToken = default);
}
